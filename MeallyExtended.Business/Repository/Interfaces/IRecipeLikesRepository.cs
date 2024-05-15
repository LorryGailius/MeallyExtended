using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Repository.Interfaces
{
    public interface IRecipeLikesRepository
    {
        Task AddRecipeLikes(Guid recipeId);
        Task RemoveRecipeLikes(Guid recipeId);
        Task AddClick(Guid recipeId);
        Task ClearClicks(Guid recipeId);
        Task<RecipeLikes?> GetRecipeLikesByRecipeId(Guid recipeId);
        IQueryable<RecipeLikes> GetRecipeLikes();
    }
}
