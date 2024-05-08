using MeallyExtended.Contracts.Dto;
using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Interfaces
{
    public interface IRecipeService
    {
        public Task<Recipe> GetRecipeById(Guid recipeId);

        public Task<Recipe> AddRecipe(RecipeDto recipe);

        public Task<Recipe> UpdateRecipe(RecipeDto recipe);

        public Task<bool> DeleteRecipe(Guid recipeId);

        public Task<IEnumerable<Recipe>> GetRecipesByCategory(List<CategoryDto> categories);

        public Task<Recipe> GetRecipeByTitle(string title);
    }
}
