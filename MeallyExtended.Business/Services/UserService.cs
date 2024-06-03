using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using MeallyExtended.Business.Operations;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.Contracts.Dto;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore;

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

        [Log]
        public async Task<PaginationResult<RecipeDto>> GetFavoriteRecipes(string userEmail, int pageNo, int pageSize)
        {
            return await GetPaginationResult(_userRepository.GetFavoriteRecipes(userEmail), pageNo, pageSize);
        }

        private async Task<PaginationResult<RecipeDto>> GetPaginationResult(IQueryable<Recipe> recipeQuery, int pageNo, int pageSize)
        {
            var totalRecipes = await recipeQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecipes / (double)pageSize);
            var recipeResult = await recipeQuery.Skip((pageNo - 1) * pageSize).Take(pageSize)
                .Include(x => x.User).Include(x => x.RecipeLikes).Include(x => x.Categories).Select(x => MeallyMapper.RecipeToDto(x)).ToListAsync();

            return new PaginationResult<RecipeDto>
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalPages = totalPages,
                Data = recipeResult,
                TotalCount = totalRecipes
            };
        }
    }
}