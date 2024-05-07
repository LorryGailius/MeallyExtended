using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Repository.Interfaces
{
    public interface IRecipeLikesRepository
    {
        public Task<RecipeLikes?> GetRecipeLikesByRecipeId(Guid recipeId);
        public Task<RecipeLikes?> AddRecipeLikes(RecipeLikes recipeLikes);
        public Task<RecipeLikes?> UpdateRecipeLikes(RecipeLikes recipeLikes);
        public Task<bool> DeleteRecipeLikes(Guid recipeLikesId);
    }
}
