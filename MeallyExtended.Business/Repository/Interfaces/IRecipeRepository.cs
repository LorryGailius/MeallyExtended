using MeallyExtended.Business.Data;
using MeallyExtended.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Business.Repository.Interfaces
{
    public interface IRecipeRepository
    {
        public Task<Recipe?> GetRecipeById(Guid recipeId);

        public Task<IEnumerable<Recipe>> GetAllRecipesByCategoryList(List<Category> categories);

        public Task<Recipe?> GetRecipeByTitle(string title);

        public Task<Recipe?> AddRecipe(Recipe recipe);

        public Task<Recipe?> UpdateRecipe(Recipe recipe);

        public Task<bool> DeleteRecipe(Guid recipeId);
    }
}
