using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Contracts.Requests.Recipe
{
    internal class CreateRecipeRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Ingredients { get; set; } = null!;
        public string Directions { get; set; } = null!;
        public required IList<Guid> CategoryId { get; set; }
    }
}
