namespace MeallyExtended.Contracts.Requests.Category
{
    internal class UpdateCategoryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
