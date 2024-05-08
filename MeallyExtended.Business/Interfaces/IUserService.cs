using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByEmail(string userEmail);
    }
}