using MeallyExtended.Business.Interfaces;
using MeallyExtended.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Business.Services
{
    public class RecipeService : IRecipeService
    {
        public bool AddRecipe(RecipeDto recipe)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRecipe(Guid recipeId)
        {
            throw new NotImplementedException();
        }

        public RecipeDto GetRecipeById(Guid recipeId)
        {
            throw new NotImplementedException();
        }

        public RecipeDto GetRecipeByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RecipeDto> GetRecipesByCategory(List<CategoryDto> categories)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRecipe(RecipeDto recipe)
        {
            throw new NotImplementedException();
        }
    }
}
