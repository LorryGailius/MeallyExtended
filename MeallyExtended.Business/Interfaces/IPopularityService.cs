using MeallyExtended.Contracts.Dto;

namespace MeallyExtended.Business.Interfaces
{
    public interface IPopularityService
    {
        Task<IEnumerable<RecipeDto>> GetPopularRecipes();
    }
}