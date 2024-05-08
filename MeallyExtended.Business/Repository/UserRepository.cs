using MeallyExtended.Business.Data;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeallyExtended.Business.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MeallyDbContext _dbContext;

        public UserRepository(MeallyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserByEmail(string userEmail)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
        }
    }
}