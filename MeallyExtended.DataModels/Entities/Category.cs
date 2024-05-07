namespace MeallyExtended.DataModels.Entities;

public class Category
{
    public required string Name { get; set; }
    public List<Recipe> Recipes { get; set; } = [];
}