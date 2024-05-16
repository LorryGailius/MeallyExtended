using MeallyExtended.Contracts.Dto;

namespace MeallyExtended.Contracts.Requests.Recipe
{
    public class CreateRecipeRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<Ingredient> Ingredients { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string Instructions { get; set; } = null!;
        public double Duration { get; set; }
        public required IList<string> Categories { get; set; }
    }
}
