using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetCategories();
        public Task<IEnumerable<string>> GetCategoryNames();

        public Task<IEnumerable<Category>> GetCategoriesByRecipeId(Guid recipeId);
        public Task<Category?> GetCategoryByName(string name);
        public Task<Category?> AddCategory(Category category);
        public Task<Category?> UpdateCategory(Category category);
        Task<IEnumerable<string>> GetQueryCategories(string query, int amount);
        public Task<bool> DeleteCategory(Guid categoryId);
    }
}
