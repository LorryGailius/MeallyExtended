﻿using System.Xml.Schema;
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

        public async Task AddFavoriteRecipe(string userEmail, Recipe recipe)
        {
            var user = await _dbContext.Users
                .Where(x => x.Email == userEmail)
                .Include(x => x.LikedRecipes)
                .FirstOrDefaultAsync();

            if (user is not null && user.LikedRecipes.All(x => x.Id != recipe.Id))
            {
                user.LikedRecipes.Add(recipe);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}