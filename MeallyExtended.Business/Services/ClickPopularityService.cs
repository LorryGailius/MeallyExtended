using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeallyExtended.Business.Services;

public class ClickPopularityService : IPopularityService
{
    private readonly IRecipeLikesRepository _recipeLikesRepository;

    public ClickPopularityService(IRecipeLikesRepository recipeLikesRepository)
    {
        _recipeLikesRepository = recipeLikesRepository;
    }

    public async Task<IEnumerable<Recipe>> GetPopularRecipes(int amount)
    {
        var popularRecipes = await _recipeLikesRepository.GetRecipeLikes()
            .OrderByDescending(x => x.ClickCount)
            .Take(amount)
            .Select(x => x.Recipe)
            .ToListAsync();

        return popularRecipes;
    }
}