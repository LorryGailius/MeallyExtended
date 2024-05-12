using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeallyExtended.Business.Services
{
    public class LikePopularityService : IPopularityService
    {
        private readonly IRecipeLikesRepository _recipeLikesRepository;

        public LikePopularityService(IRecipeLikesRepository recipeLikesRepository)
        {
            _recipeLikesRepository = recipeLikesRepository;
        }


        public async Task<IEnumerable<Recipe>> GetPopularRecipes(int amount)
        {
            var popularRecipes = await _recipeLikesRepository.GetRecipeLikes()
                .OrderByDescending(x => x.LikeCount)
                .Take(amount)
                .Select(x => x.Recipe)
                .ToListAsync();

            return popularRecipes;
        }
    }
}