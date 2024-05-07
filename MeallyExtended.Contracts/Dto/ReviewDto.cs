﻿namespace MeallyExtended.Contracts.Dto
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public required string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
