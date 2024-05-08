using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(string userEmail);
    }
}