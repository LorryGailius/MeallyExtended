namespace MeallyExtended.Contracts.Requests.Recipe
{
    internal class UpdateRecipeRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Ingredients { get; set; } = null!;
        public string Directions { get; set; } = null!;
        public required IList<Guid> CategoryId { get; set; }
    }
}
