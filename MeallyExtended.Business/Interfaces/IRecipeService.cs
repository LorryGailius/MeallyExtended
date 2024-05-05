using MeallyExtended.Contracts.Dto;
using MeallyExtended.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Business.Interfaces
{
    public interface IRecipeService
    {
        public RecipeDto GetRecipeById(Guid recipeId);

        public bool AddRecipe(RecipeDto recipe);

        public bool UpdateRecipe(RecipeDto recipe);

        public bool DeleteRecipe(Guid recipeId);

        public IEnumerable<RecipeDto> GetRecipesByCategory(List<CategoryDto> categories);

        public RecipeDto GetRecipeByTitle(string title);
    }
}
