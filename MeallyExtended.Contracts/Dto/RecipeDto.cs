using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Contracts.Dto
{
    public class RecipeDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public List<Ingredient> Ingredients { get; set; } = [];
        public string? Description { get; set; }
        public double Duration { get; set; }
        public required string Instructions { get; set; }
        public List<CategoryDto> Categories { get; set; } = [];
        public List<ReviewDto> Reviews { get; set; } = [];
        public int LikesCount { get; set; }
        public string UserEmail { get; set; } = null!;
    }
}
