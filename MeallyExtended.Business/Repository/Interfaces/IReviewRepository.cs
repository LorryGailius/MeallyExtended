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
        Task<IEnumerable<Review>> GetReviewsByRecipeId(Guid recipeId);
        Task<IEnumerable<Review>> GetReviewsByUserId(string userId);
        Task<Review> GetReviewById(Guid reviewId);
        Task<Review> AddReview(Review review);
        Task<Review> UpdateReview(Review review);
        Task<bool> DeleteReview(Guid reviewId);
    }
}
