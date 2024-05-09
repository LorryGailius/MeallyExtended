using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.Contracts.Dto;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Net.Mail;
using MeallyExtended.Contracts.Requests.Recipe;
using Microsoft.EntityFrameworkCore;

namespace MeallyExtended.Business.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeLikesRepository _recipeLikesRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserService _userService;

        public RecipeService(IRecipeRepository recipeRepository, ICategoryRepository categoryRepository, IUserService userService, IRecipeLikesRepository recipeLikesRepository)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
            _userService = userService;
            _recipeLikesRepository = recipeLikesRepository;
        }

        public async Task<Recipe> AddRecipe(RecipeDto recipe)
        {
            var validCategories = new List<Category>();

            foreach (var category in recipe.Categories)
            {
                var categoryEntity = await _categoryRepository.GetCategoryByName(category.Name);

                if (categoryEntity is not null)
                {
                    validCategories.Add(categoryEntity);
                }
            }

            if (string.IsNullOrEmpty(recipe.UserEmail))
            {
                throw new ArgumentException($"{nameof(recipe.UserEmail)} can't be null.");
            }

            var user = await _userService.GetUserByEmail(recipe.UserEmail);

            var recipeEntity = MeallyMapper.RecipeDtoToRecipe(recipe, validCategories, user);

            recipeEntity.Categories = validCategories;

            await _recipeRepository.AddRecipe(recipeEntity);

            return recipeEntity;
        }

        public async Task<bool> DeleteRecipe(Guid recipeId, string userEmail)
        {
            var recipe = await _recipeRepository.GetRecipeById(recipeId);

            if (recipe is not null && recipe.User.Email == userEmail)
            {
                await _recipeRepository.DeleteRecipe(recipeId);
                return true;
            }

            return false;
        }

        public async Task<Recipe> GetRecipeById(Guid recipeId)
        {
            var recipe = await _recipeRepository.GetRecipeById(recipeId);

            if (recipe is null)
            {
                return null;
            }

            await _recipeLikesRepository.AddClick(recipeId);

            return recipe;
        }

        public async Task<PaginationResult<RecipeDto>> GetRecipesByQuery(string? query, List<string>? categories, int pageNo = 1, int pageSize = 10)
        {
            if (categories.Count != 0 && !string.IsNullOrEmpty(query) && !string.IsNullOrWhiteSpace(query))
            {
                return await GetPaginationResult(_recipeRepository.GetRecipeByQuery(query, categories), pageNo, pageSize);

            }

            if (categories.Count == 0 || categories is null)
            {
                return await GetPaginationResult(_recipeRepository.GetRecipeByTitle(query), pageNo, pageSize);
            }

            return await GetPaginationResult(_recipeRepository.GetRecipesByCategory(categories), pageNo, pageSize);
        }

        private async Task<PaginationResult<RecipeDto>> GetPaginationResult(IQueryable<Recipe> recipeQuery, int pageNo, int pageSize)
        {
            var totalRecipes = await recipeQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecipes / (double)pageSize);
            var recipeResult = await recipeQuery.Skip((pageNo - 1) * pageSize).Take(pageSize)
                .Include(x => x.User).Include(x => x.RecipeLikes).Include(x => x.Categories).Select(x => MeallyMapper.RecipeToDto(x)).ToListAsync();

            return new PaginationResult<RecipeDto>
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalPages = totalPages,
                Data = recipeResult,
                TotalCount = totalRecipes
            };
        }

        public async Task<Recipe> UpdateRecipe(UpdateRecipeRequest recipe)
        {
            var recipeEntity = await _recipeRepository.GetRecipeById(recipe.Id);

            if (recipeEntity is null)
            {
                return null;
            }

            var validCategories = new List<Category>();

            foreach (var category in recipe.Categories)
            {
                var categoryEntity = await _categoryRepository.GetCategoryByName(category);

                if (categoryEntity is not null)
                {
                    validCategories.Add(categoryEntity);
                }
            }

            recipeEntity.Title = recipe.Title;
            recipeEntity.Description = recipe.Description;
            recipeEntity.Ingredients = [.. recipe.Ingredients];
            recipeEntity.Duration = recipe.Duration;
            recipeEntity.Instructions = recipe.Instructions;
            recipeEntity.Categories = validCategories;

            await _recipeRepository.UpdateRecipe(recipeEntity);

            return recipeEntity;
        }

        public async Task LikeRecipe(Guid recipeId, string userEmail)
        {
            var recipe = await _recipeRepository.GetRecipeById(recipeId);

            if (recipe is not null)
            {
                await _userService.AddFavoriteRecipe(userEmail, recipe);
                await _recipeLikesRepository.AddRecipeLikes(recipeId);
                return;
            }

            throw new ArgumentException("Recipe not found.");
        }

        public async Task<IEnumerable<string>> GetSearchSuggestions(string query, int amount)
        {
            return await _recipeRepository.GetSearchSuggestions(query, amount);
        }
    }
}
