using MeallyExtended.Business.Data;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore;

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

            var recipeLikes = new RecipeLikes
            {
                Recipe = recipe,
                RecipeId = recipe.Id
            };

            await _dbContext.RecipeLikes.AddAsync(recipeLikes);
            await _dbContext.Recipe.AddAsync(recipe);
            await _dbContext.SaveChangesAsync();
            return recipe;
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

        public async Task<IEnumerable<string>> GetSearchSuggestions(string query, int amount)
        {
            return await _dbContext.Recipe.Where(x => x.Title.Contains(query)).Select(x => x.Title).Take(amount)
                .ToListAsync();
        }

        public IQueryable<Recipe> GetRecipeByQuery(string query, List<string> categories)
        {
            return _dbContext.Recipe.Include(x => x.Categories)
                .Where(x => x.Categories.Any(x => categories.Any(y => y == x.Name)) && x.Title.Contains(query));
        }

        public IQueryable<Recipe> GetRecipesByCategory(List<string> categories)
        {
            return _dbContext.Recipe.Include(x => x.Categories)
                .Where(x => x.Categories.Any(x => categories.Any(y => y == x.Name)));
        }

        public IQueryable<Recipe> GetRecipeByTitle(string title)
        {
            return _dbContext.Recipe.Where(x => x.Title.Contains(title));
        }

        public async Task<Recipe?> GetRecipeById(Guid recipeId)
        {
            return await _dbContext.Recipe.Include(x => x.Categories).Include(x => x.RecipeLikes).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == recipeId);
        }

        public IQueryable<Recipe> GetQuery()
        {
            return _dbContext.Recipe;
        }

        public async Task UpdateRecipe(Recipe recipe)
        {
            _dbContext.Recipe.Update(recipe);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
