namespace MeallyExtended.Contracts.Dto
{
    public class RecipeDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public Ingredient[] Ingredients { get; set; } = [];
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public double Duration { get; set; }
        public required string Instructions { get; set; }
        public List<CategoryDto> Categories { get; set; } = [];
        public List<ReviewDto> Reviews { get; set; } = [];
        public int LikesCount { get; set; }
        public string UserEmail { get; set; } = null!;
        public bool IsFavorite { get; set; } = false;
        public byte[] Version { get; set; }
    }
}
