using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Contracts.Dto
{
    public class ReviewDto
    {
        public string UserEmail { get; set; } = null!;
        public required string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        //public RecipeDto Recipe { get; set; } = null!;
    }
}
