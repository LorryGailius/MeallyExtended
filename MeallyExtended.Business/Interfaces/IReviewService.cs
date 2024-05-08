using MeallyExtended.Contracts.Dto;
using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetLimitedReviews(Guid recipeId, int limit, int skip);
        Task<Review?> AddReview(ReviewDto review);
        Task<Review?> UpdateReview(ReviewDto review);
        Task<bool> DeleteReview(Guid reviewId);
    }
}