using Microsoft.AspNetCore.Identity;

namespace MeallyExtended.DataModels.Entities;

public class User : IdentityUser
{
    public List<Recipe> UserRecipes { get; set; } = [];

    public List<Recipe> LikedRecipes { get; set; } = [];

    public List<Review> Reviews { get; set; } = [];
}