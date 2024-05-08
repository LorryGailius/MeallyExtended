using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetLimitedReviews(Guid recipeId, int limit, int skip);
        Task<Review?> AddReview(Review review);
        Task<Review?> UpdateReview(Review review);
        Task<bool> DeleteReview(Guid reviewId);
    }
}