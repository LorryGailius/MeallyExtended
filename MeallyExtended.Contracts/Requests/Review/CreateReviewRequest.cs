﻿namespace MeallyExtended.Contracts.Requests.Review
{
    internal class CreateReviewRequest
    {
        public string UserEmail { get; set; } = null!;
        public Guid RecipeId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
