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
        private readonly IUserRepository _userRepository;

        public RecipeService(IRecipeRepository recipeRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
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

            var user = await _userRepository.GetUserByEmail(recipe.UserEmail);

            var recipeEntity = MeallyMapper.RecipeDtoToRecipe(recipe, validCategories, user);

            recipeEntity.Categories = validCategories;

            await _recipeRepository.AddRecipe(recipeEntity);

            return recipeEntity;
        }

        public async Task<bool> DeleteRecipe(Guid recipeId)
        {
            var recipe = await _recipeRepository.GetRecipeById(recipeId);

            if (recipe is not null)
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

        public async Task<PaginationResult<RecipeDto>> GetRecipesByQuery(string query, IEnumerable<CategoryDto> categories, int pageNo, int pageSize)
        {
            var categoryList = categories.Select(MeallyMapper.CategoryDtoToCategory).ToList();

            if (categoryList.Count != 0 && !string.IsNullOrEmpty(query) && !string.IsNullOrWhiteSpace(query))
            {
                return await GetPaginationResult(_recipeRepository.GetRecipeByQuery(query, categoryList), pageNo, pageSize);

            }

            if (categoryList.Count == 0)
            {
                return await GetPaginationResult(_recipeRepository.GetRecipeByTitle(query), pageNo, pageSize);
            }

            return await GetPaginationResult(_recipeRepository.GetRecipesByCategory(categoryList), pageNo, pageSize);
        }

        private async Task<PaginationResult<RecipeDto>> GetPaginationResult(IQueryable<Recipe> recipeQuery, int pageNo, int pageSize)
        {
            var totalRecipes = await recipeQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecipes / (double)pageSize);
            var recipeResult = await recipeQuery.Skip(pageNo - 1 * pageSize).Take(pageSize)
                .Select(x => MeallyMapper.RecipeToDto(x)).ToListAsync();

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

        public Task<IEnumerable<Recipe>> GetPopularRecipes()
        {
            throw new NotImplementedException();
        }
    }
}
