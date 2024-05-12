using System.Xml.Schema;
using MeallyExtended.Business.Data;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeallyExtended.Business.Repository
{
    public class RecipeLikesRepository : IRecipeLikesRepository
    {
        private readonly MeallyDbContext _dbContext;

        public RecipeLikesRepository(MeallyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRecipeLikes(Guid recipeId)
        {
            var recipeLikes = await _dbContext.RecipeLikes.FirstOrDefaultAsync(x => x.RecipeId == recipeId);

            if (recipeLikes is not null)
            {
                recipeLikes.LikeCount++;
                _dbContext.RecipeLikes.Update(recipeLikes);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveRecipeLikes(Guid recipeId)
        {
            var recipeLikes = await _dbContext.RecipeLikes.FirstOrDefaultAsync(x => x.RecipeId == recipeId);

            if (recipeLikes is not null)
            {
                recipeLikes.LikeCount--;
                _dbContext.RecipeLikes.Update(recipeLikes);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddClick(Guid recipeId)
        {
            var recipeLikes = await _dbContext.RecipeLikes.FirstOrDefaultAsync(x => x.RecipeId == recipeId);

            if (recipeLikes is not null)
            {
                recipeLikes.ClickCount++;
                _dbContext.RecipeLikes.Update(recipeLikes);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task ClearClicks(Guid recipeId)
        {
            var recipeLikes = await _dbContext.RecipeLikes.FirstOrDefaultAsync(x => x.RecipeId == recipeId);

            if (recipeLikes is not null)
            {
                recipeLikes.ClickCount = 0;
                _dbContext.RecipeLikes.Update(recipeLikes);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<RecipeLikes?> GetRecipeLikesByRecipeId(Guid recipeId)
        {
            return await _dbContext.RecipeLikes.FirstOrDefaultAsync(x => x.RecipeId == recipeId);
        }

        public IQueryable<RecipeLikes> GetRecipeLikes()
        {
            return _dbContext.RecipeLikes.Include(x => x.Recipe).Include(x => x.Recipe.User)
                .Include(x => x.Recipe.Categories);
        }
    }
}
