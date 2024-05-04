using MeallyExtended.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Contracts.Dto
{
    public class IngredientDto
    {
        public double Quantity { get; set; }
        public Units Unit { get; set; }
        public required string Name { get; set; }
    }
}
