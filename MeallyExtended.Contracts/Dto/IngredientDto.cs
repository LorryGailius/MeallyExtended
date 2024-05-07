using MeallyExtended.Contracts.Enums;

namespace MeallyExtended.Contracts.Dto
{
    public class IngredientDto
    {
        public double Quantity { get; set; }
        public Units Unit { get; set; }
        public required string Name { get; set; }
    }
}
