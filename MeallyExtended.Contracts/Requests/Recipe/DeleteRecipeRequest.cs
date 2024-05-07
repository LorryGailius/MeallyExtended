using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Contracts.Requests.Recipe
{
    internal class DeleteRecipeRequest
    {
        public Guid Id { get; set; }
    }
}
