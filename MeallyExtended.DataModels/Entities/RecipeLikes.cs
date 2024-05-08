namespace MeallyExtended.DataModels.Entities;

public class RecipeLikes
{
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    public int LikeCount { get; set; }
    public int ClickCount { get; set; }
}