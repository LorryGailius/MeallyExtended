namespace MeallyExtended.Contracts.Requests.Recipe
{
    public class CreateRecipeRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Ingredients { get; set; } = null!;
        public string Directions { get; set; } = null!;
        public required IList<Guid> CategoryId { get; set; }
    }
}
