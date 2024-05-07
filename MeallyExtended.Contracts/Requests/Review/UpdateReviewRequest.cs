using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Contracts.Requests.Review
{
    internal class UpdateReviewRequest
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public Guid RecipeId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
