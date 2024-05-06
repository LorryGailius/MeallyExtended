using MeallyExtended.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Business.Repository.Interfaces
{
    public interface IRecipeLikesRepository
    {
        public Task<RecipeLikes> GetRecipeLikesByRecipeId(Guid recipeId);
        public Task<RecipeLikes> GetRecipeLikesById(Guid recipeLikesId);
        public Task<RecipeLikes> AddRecipeLikes(RecipeLikes recipeLikes);
        public Task<RecipeLikes> UpdateRecipeLikes(RecipeLikes recipeLikes);
        public Task<bool> DeleteRecipeLikes(Guid recipeLikesId);
    }
}
