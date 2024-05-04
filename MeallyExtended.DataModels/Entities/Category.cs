﻿namespace MeallyExtended.DataModels.Entities;

public class Category
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public List<Recipe> Recipes { get; set; } = [];
}