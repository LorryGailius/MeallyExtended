namespace MeallyExtended.Contracts.Requests.Review
{
    public class CreateReviewRequest
    {
        public Guid RecipeId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
