﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Contracts.Dto
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
