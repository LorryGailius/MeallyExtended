using MeallyExtended.Contracts.Dto;
using MeallyExtended.Contracts.Requests.Recipe;
using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Interfaces
{
    public interface IRecipeService
    {
        Task<Recipe> AddRecipe(RecipeDto recipe);
        Task DeleteRecipe(Guid recipeId, string userEmail);
        Task<Recipe> GetRecipeById(Guid recipeId);
        Task<PaginationResult<RecipeDto>> GetRecipesByQuery(string query, List<string> categories, int pageNo, int pageSize);
        Task<Recipe> UpdateRecipe(UpdateRecipeRequest recipe, string userEmail);
        Task<PaginationResult<RecipeDto>> GetBrowseRecipes(int pageNo, int pageSize);
        Task LikeRecipe(Guid recipeId, string userEmail);
        Task<IEnumerable<string>> GetSearchSuggestions(string query, int amount);
    }
}
