using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Interfaces
{
    public interface IPopularityService
    {
        Task<IEnumerable<Recipe>> GetPopularRecipes(int amount);
    }
}