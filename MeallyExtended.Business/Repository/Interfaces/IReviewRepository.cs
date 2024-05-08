using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Repository.Interfaces
{
    public interface IReviewRepository
    {
        public Task<IEnumerable<Review>> GetReviewsByRecipeId(Guid recipeId);
        public Task<IEnumerable<Review>> GetReviewsByUserId(string userId);
        public Task<Review?> GetReviewById(Guid reviewId);
        public Task<Review?> AddReview(Review review);
        public Task<Review?> UpdateReview(Review review);
        public Task<bool> DeleteReview(Guid reviewId);
    }
}
