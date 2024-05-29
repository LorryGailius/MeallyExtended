using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<Category?> GetCategoryByName(string name);
        Task<IEnumerable<string>> GetQueryCategories(string query, int amount);
        Task<IEnumerable<string>> GetCategoryNames();
        Task<IEnumerable<Category>> GetCategories();
    }
}