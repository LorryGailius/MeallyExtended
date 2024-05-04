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
    public class RecipeEntityConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipe");

            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.User).WithMany(r => r.UserRecipes).HasForeignKey(r => r.UserId);

            builder.Property(r => r.Title).IsRequired().HasMaxLength(200);
            builder.Property(r => r.Duration).IsRequired();
            builder.Property(r => r.Instructions).IsRequired().HasMaxLength(4000);

        }
    }
}
