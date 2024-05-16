using System.ComponentModel.DataAnnotations;

namespace MeallyExtended.DataModels.Entities;

public class Review
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; } = null!;
    public required string Text { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set;}
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    [Timestamp]
    public byte[] Version { get; set; }
}