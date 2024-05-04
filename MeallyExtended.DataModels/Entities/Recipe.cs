using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeallyExtended.DataModels.Entities;

public class Recipe
{
    public Guid Id { get; set; }
    public required string Title { get; set; }

    [NotMapped]
    public Ingredient[]? Ingredients { get; set; } = [];

    [Column(TypeName = "nvarchar(max)")]
    public string IngredientsJson
    {
        get => JsonConvert.SerializeObject(Ingredients);
        set => Ingredients = JsonConvert.DeserializeObject<Ingredient[]>(value);
    }

    public string? Description { get; set; }
    public double Duration { get; set; } 
    public required string Instructions { get; set; }
    public List<Category> Categories { get; set; } = [];
    public List<Review> Reviews { get; set; } = [];
    public RecipeLikes RecipeLikes { get; set; } = null!;

    public List<User> UsersLiked { get; set; } = null!;

    public string? UserId { get; set; }
    public User User { get; set; } = null!;
}