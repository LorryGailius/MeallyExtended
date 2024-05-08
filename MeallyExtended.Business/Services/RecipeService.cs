using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.Contracts.Dto;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Net.Mail;

namespace MeallyExtended.Business.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
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

        public bool DeleteRecipe(Guid recipeId)
        {
            _recipeRepository.DeleteRecipe(recipeId);
            throw new NotImplementedException();

        }

        public Task<Recipe> GetRecipeById(Guid recipeId)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> GetRecipeByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByCategory(List<CategoryDto> categories)
        {
            throw new NotImplementedException();
        }

        public async Task<Recipe> UpdateRecipe(RecipeDto recipe)
        {
            throw new NotImplementedException();
        }
    }
}
