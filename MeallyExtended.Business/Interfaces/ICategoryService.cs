using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<Category?> GetCategoryByName(string name);
    }
}