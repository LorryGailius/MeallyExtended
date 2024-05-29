using MeallyExtended.Contracts.Dto;
using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByEmail(string userEmail);
        Task AddFavoriteRecipe(string userEmail, Recipe recipe);
        Task RemoveFavoriteRecipe(string userEmail, Recipe recipe);
        Task<PaginationResult<RecipeDto>> GetFavoriteRecipes(string userEmail, int pageNo, int pageSize);
    }
}