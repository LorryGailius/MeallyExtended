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
    public class RecipeRepository : IRecipeRepository
    {
        private readonly MeallyDbContext _dbContext;

        public RecipeRepository(MeallyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                // Todo: in future maybe throw exception, assume error case will be checked in service?(bad for reusability in this case)
                return false;
            }

            _dbContext.Recipe.Add(recipe);
            return _dbContext.SaveChanges() != 0;
        }

        public bool DeleteRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                // Todo: in future maybe throw exception, assume error case will be checked in service?(bad for reusability in this case)
                return false;
            }

            _dbContext.Recipe.Remove(recipe);
            return _dbContext.SaveChanges() != 0;
        }

        public bool DeleteRecipe(Guid recipeId)
        {
            var recipe = _dbContext.Recipe.FirstOrDefault(r => r.Id == recipeId);
            if (recipe == null)
            {
                // Todo: in future maybe throw exception, assume error case will be checked in service?(bad for reusability in this case)
                return false;
            }

            _dbContext.Recipe.Remove(recipe);
            return _dbContext.SaveChanges() != 0;
        }

        public IEnumerable<Recipe> GetAllRecipesByCategoryList(List<Category> categories)
        {
            return _dbContext.Recipe.Include(x => x.Categories).Where(x => x.Categories.Any(y => categories.Contains(y)));
        }

        public Recipe? GetRecipeById(Guid recipeId)
        {
            return _dbContext.Recipe.FirstOrDefault(x => x.Id == recipeId); 
        }

        public Recipe? GetRecipeByTitle(string title)
        {
            return _dbContext.Recipe.FirstOrDefault(x => x.Title.Contains(title, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool UpdateRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                // Todo: in future maybe throw exception, assume error case will be checked in service?(bad for reusability in this case)
                return false;
            }

            _dbContext.Recipe.Update(recipe);
            return _dbContext.SaveChanges() != 0;
        }
    }
}
