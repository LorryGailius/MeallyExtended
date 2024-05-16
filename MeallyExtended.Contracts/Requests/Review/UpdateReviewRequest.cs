namespace MeallyExtended.Contracts.Requests.Review
{
    public class UpdateReviewRequest
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public byte[] Version { get; set; }
    }
}
