namespace MeallyExtended.Contracts.Requests.Review
{
    public class UpdateReviewRequest
    {
        public Guid Id { get; set; }
        public Guid RecipeId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }
        public byte[] Version { get; set; }
    }
}
