using MeallyExtended.Contracts.Dto;

namespace MeallyExtended.Contracts.Requests.Recipe
{
    public class UpdateRecipeRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<Ingredient> Ingredients { get; set; } = null!;
        public string Instructions { get; set; } = null!;
        public double Duration { get; set; }
        public required IList<string> Categories { get; set; }
    }
}
