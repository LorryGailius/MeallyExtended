using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string userEmail);
        Task AddFavoriteRecipe(string userEmail, Recipe recipe);
        IQueryable<Recipe> GetFavoriteRecipes(string userEmail);
        Task RemoveFavoriteRecipe(string userEmail,Recipe recipe);
    }
}