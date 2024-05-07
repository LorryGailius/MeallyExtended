using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Contracts.Requests.Category
{
    internal class UpdateCategoryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
