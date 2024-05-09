using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Services
{
    public class UserService
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

        public async Task AddFavoriteRecipe(string userId, Recipe recipe)
        {
            await _userRepository.AddFavoriteRecipe(userId, recipe);
        }
    }
}