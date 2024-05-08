using MeallyExtended.Contracts.Dto;
using MeallyExtended.DataModels.Entities;
using MeallyExtended.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeallyExtended.Contracts.Requests.Recipe;
using MeallyExtended.Contracts.Requests.Review;
using MeallyExtended.Contracts.Requests.Category;

namespace MeallyExtended.Business.Mappers
{
    public static class MeallyMapper
    {
        #region Category

        public static CategoryDto CategoryToDto(Category category)
        {
            return new CategoryDto
            {
                Name = category.Name
            };
        }

        public static Category CategoryDtoToCategory(CategoryDto categoryDto)
        {
            return new Category
            {
                Name = categoryDto.Name
            };
        }

        public static CategoryDto CreateCategoryRequestToCategoryDto(CreateCategoryRequest createCategoryRequest)
        {
            return new CategoryDto
            {
                Name = createCategoryRequest.Name
            };
        }

        public static CategoryDto UpdateCategoryRequestToCategoryDto(UpdateCategoryRequest updateCategoryRequest)
        {
            return new CategoryDto
            {
                Name = updateCategoryRequest.Name
            };
        }
        #endregion Category

        #region Review

        public static ReviewDto ReviewToDto(Review review)
        {
            return new ReviewDto
            {
                Id = review.Id,
                UserEmail = review.User.Email,
                Text = review.Text,
                CreatedDate = review.CreatedDate
            };
        }

        public static Review ReviewDtoToReview(ReviewDto reviewDto)
        {
            return new Review
            {
                Id = reviewDto.Id,
                Text = reviewDto.Text,
                CreatedDate = reviewDto.CreatedDate
            };
        }

        public static ReviewDto CreateReviewRequestToReviewDto(CreateReviewRequest createReviewRequest)
        {
            return new ReviewDto
            {
                RecipeId = createReviewRequest.RecipeId,
                UserEmail = createReviewRequest.UserEmail,
                Text = createReviewRequest.Text,
                CreatedDate = createReviewRequest.CreatedDate
            };
        }

        public static ReviewDto UpdateReviewRequestToReviewDto(UpdateReviewRequest updateReviewRequest)
        {
            return new ReviewDto
            {
                RecipeId = updateReviewRequest.RecipeId,
                UserEmail = updateReviewRequest.UserEmail,
                Text = updateReviewRequest.Text,
                CreatedDate = updateReviewRequest.CreatedDate
            };
        }
        #endregion Review

        #region Recipe
        public static RecipeDto RecipeToDto(Recipe recipe)
        {
            return new RecipeDto
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients.ToList(),
                Instructions = recipe.Instructions,
                LikesCount = recipe.RecipeLikes.LikeCount,
                Duration = recipe.Duration,
                Categories = recipe.Categories.Select(CategoryToDto).ToList(),
                Reviews = recipe.Reviews.Select(ReviewToDto).ToList(),
            };
        }

        public static Recipe RecipeDtoToRecipe(RecipeDto recipeDto)
        {
            return new Recipe
            {
                Id = recipeDto.Id,
                Title = recipeDto.Title,
                Description = recipeDto.Description,
                Ingredients = recipeDto.Ingredients.ToArray(),
                Instructions = recipeDto.Instructions,
                Duration = recipeDto.Duration,
                Categories = recipeDto.Categories.Select(c => new Category {Name = c.Name }).ToList(),
                Reviews = recipeDto.Reviews.Select(r => new Review { Id = r.Id, Text = r.Text, CreatedDate = r.CreatedDate }).ToList()
            };
        }

        public static RecipeDto CreateRecipeRequestToRecipeDto(CreateRecipeRequest createRecipeRequest)
        {
            return new RecipeDto
            {
                Title = createRecipeRequest.Title,
                Description = createRecipeRequest.Description,
                Ingredients = createRecipeRequest.Ingredients,
                Instructions = createRecipeRequest.Instructions,
                Duration = createRecipeRequest.Duration,
                Categories = createRecipeRequest.Categories.Select(c => new CategoryDto { Name = c }).ToList()
            };
        }

        public static RecipeDto UpdateRecipeRequestToRecipeDto(UpdateRecipeRequest updateRecipeRequest)
        {
            return new RecipeDto
            {
                Id = updateRecipeRequest.Id,
                Title = updateRecipeRequest.Title,
                Description = updateRecipeRequest.Description,
                Ingredients = updateRecipeRequest.Ingredients,
                Instructions = updateRecipeRequest.Instructions,
                Duration = updateRecipeRequest.Duration,
                Categories = updateRecipeRequest.Categories.Select(c => new CategoryDto { Name = c }).ToList()
            };
        }
        #endregion Recipe
    }
}
