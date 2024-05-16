using System.ComponentModel.DataAnnotations;
using MeallyExtended.Contracts.Dto;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

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
        get => JsonSerializer.Serialize(Ingredients);
        set => Ingredients = JsonSerializer.Deserialize<Ingredient[]>(value);
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

    [Timestamp]
    public byte[] Version { get; set; }
}