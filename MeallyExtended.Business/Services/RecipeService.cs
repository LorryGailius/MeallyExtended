using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.Contracts.Dto;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MeallyExtended.Business.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICategoryRepository _categoryRepository;

        public RecipeService(IRecipeRepository recipeRepository, ICategoryRepository categoryRepository)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
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

            var recipeEntity = MeallyMapper.RecipeDtoToRecipe(recipe, validCategories);

            recipeEntity.Categories = validCategories;

            await _recipeRepository.AddRecipe(recipeEntity);

            return recipeEntity;
        }

        public bool DeleteRecipe(Guid recipeId)
        {
            throw new NotImplementedException();
        }

        public RecipeDto GetRecipeById(Guid recipeId)
        {
            throw new NotImplementedException();
        }

        public RecipeDto GetRecipeByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RecipeDto> GetRecipesByCategory(List<CategoryDto> categories)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRecipe(RecipeDto recipe)
        {
            throw new NotImplementedException();
        }
    }
}
