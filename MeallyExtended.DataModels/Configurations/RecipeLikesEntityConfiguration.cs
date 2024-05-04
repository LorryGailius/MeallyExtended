using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.DataModels.Configurations
{
    public class RecipeLikesEntityConfiguration : IEntityTypeConfiguration<RecipeLikes>
    {
        public void Configure(EntityTypeBuilder<RecipeLikes> builder)
        {
            
        }
    }
}
