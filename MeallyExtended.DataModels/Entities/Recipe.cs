namespace MeallyExtended.DataModels.Entities;

public class Recipe
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public List<Ingredient> Ingredients { get; set; } = [];
    public string? Description { get; set; }
    public double Duration { get; set; } 
    public required string Instructions { get; set; }
    public List<Category> Categories { get; set; } = [];
    public List<Review> Reviews { get; set; } = [];
    public RecipeLikes LikeCount { get; set; } = null!;
}