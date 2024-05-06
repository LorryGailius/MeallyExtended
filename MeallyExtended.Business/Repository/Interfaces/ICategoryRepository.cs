﻿using MeallyExtended.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Business.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetCategories();
        public Task<IEnumerable<Category>> GetCategoriesByRecipeId(Guid recipeId);
        public Task<Category?> GetCategoryById(Guid categoryId);
        public Task<Category?> AddCategory(Category category);
        public Task<Category?> UpdateCategory(Category category);
        public Task<bool> DeleteCategory(Guid categoryId);
    }
}
