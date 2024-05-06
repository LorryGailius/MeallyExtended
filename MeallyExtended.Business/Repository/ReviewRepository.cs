using MeallyExtended.Business.Data;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Review>> GetReviewsByUserId(string userId)
        {
            return await _dbContext.Review
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Review> GetReviewById(Guid reviewId)
        {
            var review = await _dbContext.Review.FindAsync(reviewId);
            if (review == null) throw new KeyNotFoundException("Review not found.");
            return review;
        }

        public async Task<Review> AddReview(Review review)
        {
            if (review == null) throw new ArgumentNullException(nameof(review));

            var addedReview = _dbContext.Review.Add(review).Entity;
            await _dbContext.SaveChangesAsync();
            return addedReview;
        }

        public async Task<Review> UpdateReview(Review review)
        {
            if (review == null) throw new ArgumentNullException(nameof(review));

            var existingReview = await _dbContext.Review.FindAsync(review.Id);
            if (existingReview == null) throw new KeyNotFoundException("Review to update not found.");

            _dbContext.Entry(existingReview).CurrentValues.SetValues(review);
            await _dbContext.SaveChangesAsync();
            return existingReview;
        }

        public async Task<bool> DeleteReview(Guid reviewId)
        {
            var review = await _dbContext.Review.FindAsync(reviewId);
            if (review == null) throw new KeyNotFoundException("Review to delete not found.");

            _dbContext.Review.Remove(review);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}