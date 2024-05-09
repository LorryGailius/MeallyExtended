using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Repository.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetCategories();
        public Task<IEnumerable<Category>> GetCategoriesByRecipeId(Guid recipeId);
        public Task<Category?> GetCategoryByName(string name);
        public Task<Category?> AddCategory(Category category);
        public Task<Category?> UpdateCategory(Category category);
        public Task<bool> DeleteCategory(Guid categoryId);
    }
}
