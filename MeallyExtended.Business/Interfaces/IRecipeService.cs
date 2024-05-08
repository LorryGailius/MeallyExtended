using MeallyExtended.Contracts.Dto;
using MeallyExtended.Contracts.Requests.Recipe;
using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Interfaces
{
    public interface IRecipeService
    {
        Task<Recipe> AddRecipe(RecipeDto recipe);
        Task<bool> DeleteRecipe(Guid recipeId);
        Task<Recipe> GetRecipeById(Guid recipeId);
        Task<PaginationResult<RecipeDto>> GetRecipesByQuery(string query, IEnumerable<CategoryDto> categories, int pageNo, int pageSize);
        Task<Recipe> UpdateRecipe(UpdateRecipeRequest recipe);
    }
}
