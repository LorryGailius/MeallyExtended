using MeallyExtended.DataModels.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace MeallyExtended.Business.Data
{
    public class MeallyDbContext(DbContextOptions<MeallyDbContext> options) :
            IdentityDbContext<User>(options)
    {

        public DbSet<Category> Category { get; set; }

        public DbSet<Recipe> Recipe { get; set; }

        public DbSet<RecipeLikes> RecipeLikes { get; set; }

        public DbSet<Review> Review { get; set; }

     
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name).IsRequired().HasMaxLength(450);
            });

            builder.Entity<Recipe>(entity =>
            {
                entity.ToTable("Recipe");

                entity.HasKey(r => r.Id);

                entity.HasMany(r => r.Reviews).WithOne(r => r.Recipe).HasForeignKey(r => r.RecipeId).IsRequired();
                entity.HasMany(r => r.Categories).WithMany(r => r.Recipes);

                entity.HasOne(r => r.RecipeLikes).WithOne(r => r.Recipe).HasForeignKey<RecipeLikes>(r => r.RecipeId).IsRequired();


                entity.Property(r => r.Title).IsRequired().HasMaxLength(200);
                entity.Property(r => r.Duration).IsRequired();
                entity.Property(r => r.Instructions).IsRequired().HasMaxLength(4000);
            });

            builder.Entity<RecipeLikes>(entity =>
            {
                entity.ToTable("RecipeLikes");

                entity.HasKey(r => r.RecipeId);

            });

            builder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.HasKey(r => r.Id);

                entity.Property(r => r.Text).IsRequired().HasMaxLength(4000);
            });

            builder.Entity<User>(entity =>
            {
                entity.HasMany(r => r.UserRecipes).WithOne(r => r.User).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(r => r.Reviews).WithOne(r => r.User).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                entity.HasMany(r => r.LikedRecipes).WithMany(r => r.UsersLiked);
            });
        }
    }

}
