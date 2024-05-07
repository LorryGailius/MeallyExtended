using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Contracts.Requests.Category
{
    internal class CreateCategoryRequest
    {
        public string Name { get; set; } = null!;
    }
}
