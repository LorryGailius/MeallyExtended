using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByEmail(string userEmail)
        {
            return await _userRepository.GetUserByEmail(userEmail);
        }

        public async Task AddFavoriteRecipe(string userEmail, Recipe recipe)
        {
            await _userRepository.AddFavoriteRecipe(userEmail, recipe);
        }

        public async Task RemoveFavoriteRecipe(string userEmail, Recipe recipe)
        {
            await _userRepository.RemoveFavoriteRecipe(userEmail, recipe);
        }

        public async Task<List<Recipe>> GetFavoriteRecipes(string userEmail)
        {
            return await _userRepository.GetFavoriteRecipes(userEmail);
        }
    }
}