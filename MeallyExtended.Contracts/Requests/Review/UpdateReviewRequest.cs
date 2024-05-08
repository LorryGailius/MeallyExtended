namespace MeallyExtended.Contracts.Requests.Review
{
    public class UpdateReviewRequest
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public Guid RecipeId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
