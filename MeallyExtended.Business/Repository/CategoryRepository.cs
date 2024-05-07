using MeallyExtended.Business.Data;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeallyExtended.Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MeallyDbContext _dbContext;

        public CategoryRepository(MeallyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _dbContext.Category.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesByRecipeId(Guid recipeId)
        {
            return await _dbContext.Category
                .Where(c => c.Recipes.Any(r => r.Id == recipeId)).ToListAsync();
        }

        public async Task<Category?> GetCategoryById(Guid categoryId)
        {
            return await _dbContext.Category
                .FindAsync(categoryId);
        }

        public async Task<Category?> AddCategory(Category category)
        {
            if (category == null)
            {
                return null;
            }
            
            var addedCategory = _dbContext.Category.Add(category).Entity;
            await _dbContext.SaveChangesAsync();
            return addedCategory;
        }

        public async Task<Category?> UpdateCategory(Category category)
        {
            if (category == null)
            {
                return null;
            }

            var updatedCategory = _dbContext.Category.Update(category).Entity;
            await _dbContext.SaveChangesAsync();
            return updatedCategory;
        }

        public async Task<bool> DeleteCategory(Guid categoryId)
        {
            var category = await _dbContext.Category.FindAsync(categoryId);
            if (category == null)
            {
                return false;
            }

            _dbContext.Category.Remove(category);
            return await _dbContext.SaveChangesAsync() != 0;

        }
    }
}
