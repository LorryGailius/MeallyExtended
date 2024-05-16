using MeallyExtended.Contracts.Dto;
using MeallyExtended.Contracts.Requests.Review;
using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetLimitedReviews(Guid recipeId, int limit, int skip);
        Task<Review?> AddReview(ReviewDto review);
        Task<Review?> UpdateReview(UpdateReviewRequest review, string userEmail);
        Task<bool> DeleteReview(Guid reviewId, string userEmail);
    }
}