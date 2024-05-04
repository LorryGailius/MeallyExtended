using MeallyExtended.Contracts.Enums;

namespace MeallyExtended.DataModels.Entities;

public class Ingredient
{
    public double Quantity { get; set; }
    public Units Unit { get; set; }
    public required string Name { get; set; }
}