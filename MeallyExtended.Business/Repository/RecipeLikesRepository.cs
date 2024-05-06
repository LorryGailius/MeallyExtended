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
    public class RecipeLikesRepository : IRecipeLikesRepository
    {
        private readonly MeallyDbContext _dbContext;

        public RecipeLikesRepository(MeallyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RecipeLikes?> AddRecipeLikes(RecipeLikes recipeLikes)
        {
            if (recipeLikes == null)
            {
                // Todo: in future maybe throw exception, assume error case will be checked in service?(bad for reusability in this case)
                return null;
            }

            RecipeLikes savedRecipeLikes = _dbContext.RecipeLikes.Add(recipeLikes).Entity;
            await _dbContext.SaveChangesAsync();
            return savedRecipeLikes;
        }

        public async Task<bool> DeleteRecipeLikes(Guid recipeLikesId)
        {
            var recipeLikes = _dbContext.RecipeLikes.FirstOrDefault(r => r.RecipeId == recipeLikesId);
            if (recipeLikes == null)
            {
                // Todo: in future maybe throw exception, assume error case will be checked in service?(bad for reusability in this case)
                return false;
            }

            _dbContext.RecipeLikes.Remove(recipeLikes);
            return await _dbContext.SaveChangesAsync() != 0;
        }


        public async Task<RecipeLikes?> GetRecipeLikesByRecipeId(Guid recipeId)
        {
            return await _dbContext.RecipeLikes.FirstOrDefaultAsync(x => x.RecipeId == recipeId);
        }

        public async Task<RecipeLikes?> UpdateRecipeLikes(RecipeLikes recipeLikes)
        {
            if (recipeLikes == null)
            {
                // Todo: in future maybe throw exception, assume error case will be checked in service?(bad for reusability in this case)
                return null;
            }

            RecipeLikes changedRecipe = _dbContext.RecipeLikes.Update(recipeLikes).Entity;
            await _dbContext.SaveChangesAsync();
            return changedRecipe;
        }
    }
}
