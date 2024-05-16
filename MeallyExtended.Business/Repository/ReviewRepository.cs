using MeallyExtended.Business.Data;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeallyExtended.Business.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MeallyDbContext _dbContext;

        public ReviewRepository(MeallyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Review>> GetReviewsByRecipeId(Guid recipeId)
        {
            return await _dbContext.Review
                .Where(r => r.RecipeId == recipeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetLimitedReviews(Guid recipeId, int limit, int skip)
        {
            return await _dbContext.Review
                .Where(r => r.RecipeId == recipeId)
                .OrderBy(x => x.ModifiedDate)
                .ThenBy(x => x.CreatedDate)
                .Skip(skip)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserId(string userId)
        {
            return await _dbContext.Review
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Review?> GetReviewById(Guid reviewId)
        {
            var review = await _dbContext.Review.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == reviewId);
            if (review == null)
            {
                return null;
            }
            return review;
        }

        public async Task<Review?> AddReview(Review review)
        {
            if (review == null)
            {
                return null;
            }

            var addedReview = _dbContext.Review.Add(review).Entity;
            await _dbContext.SaveChangesAsync();
            return addedReview;
        }

        public async Task<Review?> UpdateReview(Review review)
        {
            if (review == null)
            {
                return null;
            }

            Review updatedReview = _dbContext.Review.Update(review).Entity;
            await _dbContext.SaveChangesAsync();
            return updatedReview;
        }

        public async Task<bool> DeleteReview(Guid reviewId)
        {
            var review = await _dbContext.Review.FindAsync(reviewId);
            if (review == null)
            {
                return false;
            }

            _dbContext.Review.Remove(review);
            return await _dbContext.SaveChangesAsync() != 0;
        }
    }
}