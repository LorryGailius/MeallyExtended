using MeallyExtended.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
