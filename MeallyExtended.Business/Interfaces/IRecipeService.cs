using MeallyExtended.Contracts.Dto;
using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Interfaces
{
    public interface IRecipeService
    {
        public RecipeDto GetRecipeById(Guid recipeId);

        public Task<Recipe> AddRecipe(RecipeDto recipe);

        public bool UpdateRecipe(RecipeDto recipe);

        public bool DeleteRecipe(Guid recipeId);

        public IEnumerable<RecipeDto> GetRecipesByCategory(List<CategoryDto> categories);

        public RecipeDto GetRecipeByTitle(string title);
    }
}
