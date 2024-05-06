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
        public Recipe? GetRecipeById(Guid recipeId);

        public IEnumerable<Recipe> GetAllRecipesByCategoryList(List<Category> categories);

        public Recipe? GetRecipeByTitle(string title);

        public bool AddRecipe(Recipe recipe);

        public bool UpdateRecipe(Recipe recipe);

        public bool DeleteRecipe(Recipe recipe);

        public bool DeleteRecipe(Guid recipeId);
    }
}
