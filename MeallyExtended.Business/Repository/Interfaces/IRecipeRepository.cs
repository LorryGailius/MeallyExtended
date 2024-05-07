﻿using MeallyExtended.Business.Data;
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
        Task<Recipe?> AddRecipe(Recipe recipe);
        Task<bool> DeleteRecipe(Guid recipeId);
        Task<IEnumerable<string>> GetSearchSuggestions(string query, int amount);
        IQueryable<Recipe> GetRecipeByQuery(string query, List<Category> categories);
        IQueryable<Recipe> GetRecipesByCategory(List<Category> categories);
        IQueryable<Recipe> GetRecipeByTitle(string title);
        Task<Recipe?> GetRecipeById(Guid recipeId);
        Task<Recipe?> UpdateRecipe(Recipe recipe);
    }
}
