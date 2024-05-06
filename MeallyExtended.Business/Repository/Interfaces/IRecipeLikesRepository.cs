using MeallyExtended.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Business.Repository.Interfaces
{
    public interface IRecipeLikesRepository
    {
        Task<RecipeLikes> GetRecipeLikesByRecipeId(Guid recipeId);
        Task<RecipeLikes> GetRecipeLikesById(Guid recipeLikesId);
        Task<RecipeLikes> AddRecipeLikes(RecipeLikes recipeLikes);
        Task<RecipeLikes> UpdateRecipeLikes(RecipeLikes recipeLikes);
        Task<bool> DeleteRecipeLikes(Guid recipeLikesId);
    }
}
