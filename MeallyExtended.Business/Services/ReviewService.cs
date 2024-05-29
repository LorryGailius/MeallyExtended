using System.Data;
using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.Contracts.Dto;
using MeallyExtended.Contracts.Requests.Recipe;
using MeallyExtended.Contracts.Requests.Review;
using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRecipeRepository _recipeRepository;

    public ReviewService(IReviewRepository reviewRepository, IUserRepository userRepository, IRecipeRepository recipeRepository)
    {
        _reviewRepository = reviewRepository;
        _userRepository = userRepository;
        _recipeRepository = recipeRepository;
    }

    public async Task<Review?> AddReview(ReviewDto review)
    {
        Review reviewEntity = MeallyMapper.ReviewDtoToReview(review);

        User? user = await _userRepository.GetUserByEmail(review.UserEmail);
        if (user == null)
        {
            throw new ArgumentException($"No user found for email: {review.UserEmail}");
        }

        reviewEntity.UserId = user.Id;

        return await _reviewRepository.AddReview(reviewEntity);
    }

    public async Task<bool> DeleteReview(Guid reviewId, string userEmail)
    {
        Review? reviewToDelete = await _reviewRepository.GetReviewById(reviewId);
        if (reviewToDelete == null)
        {
            throw new ArgumentException($"No such review found with ID: {reviewId}.");
        }

        if (reviewToDelete.User.Email  != userEmail)
        {
            throw new ArgumentException("Can't delete review because provided user is not creator of the review.");
        }

        return await _reviewRepository.DeleteReview(reviewId);
    }

    public async Task<IEnumerable<Review>> GetLimitedReviews(Guid recipeId, int limit, int skip)
    {
        if (skip < 0 || limit <= 0)
        {
            throw new ArgumentException($"Values do not match constraints: skip < 0 | limit <= 0");
        }

        if (await _recipeRepository.GetRecipeById(recipeId) == null)
        {
            throw new ArgumentException($"No such recipe found, ID: {recipeId}");
        }

        return await _reviewRepository.GetLimitedReviews(recipeId, limit, skip);
    }

    public async Task<Review?> UpdateReview(UpdateReviewRequest review, string userEmail)
    {
        var reviewToUpdate = await _reviewRepository.GetReviewById(review.Id);
        var user = await _userRepository.GetUserByEmail(userEmail);

        if (user == null)
        {
            throw new ArgumentException($"No such user found with email: {userEmail}.");
        }

        if (reviewToUpdate == null)
        {
            throw new ArgumentException($"No such review found, ID: {review.Id}");
        }

        if (user.Id != reviewToUpdate.UserId)
        {
            throw new ArgumentException("Can't delete review because provided user is not creator of the review.");
        }

        if (!review.Version.SequenceEqual(reviewToUpdate.Version))
        {
            throw new DBConcurrencyException("Review has already been updated.");
        }

        reviewToUpdate.Text = review.Text;
        reviewToUpdate.ModifiedDate = DateTime.Now;

        return await _reviewRepository.UpdateReview(reviewToUpdate);
    }
}