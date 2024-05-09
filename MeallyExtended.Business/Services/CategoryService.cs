using MeallyExtended.Business.Interfaces;
using MeallyExtended.Data.Repositories.Interfaces;
using MeallyExtended.DataModels.Entities;
using System;
using System.Threading.Tasks;

namespace MeallyExtended.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<Category?> GetCategoryByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty.", nameof(name));

            return await _categoryRepository.GetCategoryByName(name);
        }
    }
}
