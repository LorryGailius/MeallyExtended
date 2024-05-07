using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Contracts.Dto
{
    public class RecipeLikesDto
    {
        public Guid RecipeId { get; set; }
        public int LikeCount { get; set; }
    }
}
