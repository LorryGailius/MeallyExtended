using MeallyExtended.Business.Data;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Business.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly MeallyDbContext _dbContext;

        public RecipeRepository(MeallyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Recipe?> AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                // Todo: in future maybe throw exception, assume error case will be checked in service?(bad for reusability in this case)
                return null;
            }

            Recipe savedRecipe = _dbContext.Recipe.Add(recipe).Entity;
            await _dbContext.SaveChangesAsync();
            return savedRecipe;
        }

        public async Task<bool> DeleteRecipe(Guid recipeId)
        {
            var recipe = _dbContext.Recipe.FirstOrDefault(r => r.Id == recipeId);
            if (recipe == null)
            {
                // Todo: in future maybe throw exception, assume error case will be checked in service?(bad for reusability in this case)
                return false;
            }

            _dbContext.Recipe.Remove(recipe);
            return await _dbContext.SaveChangesAsync() != 0;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesByCategoryList(List<Category> categories)
        {
            return await _dbContext.Recipe.Include(x => x.Categories).Where(x => x.Categories.Any(y => categories.Contains(y))).ToListAsync();
        }

        public async Task<Recipe?> GetRecipeById(Guid recipeId)
        {
            return await _dbContext.Recipe.FirstOrDefaultAsync(x => x.Id == recipeId); 
        }

        public async Task<Recipe?> GetRecipeByTitle(string title)
        {
            return await _dbContext.Recipe.FirstOrDefaultAsync(x => x.Title.Contains(title, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<Recipe?> UpdateRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                // Todo: in future maybe throw exception, assume error case will be checked in service?(bad for reusability in this case)
                return null;
            }

            Recipe changedRecipe = _dbContext.Recipe.Update(recipe).Entity;
            await _dbContext.SaveChangesAsync();
            return changedRecipe;
        }
    }
}
