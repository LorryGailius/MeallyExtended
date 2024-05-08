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
                RecipeId = reviewDto.RecipeId,
                CreatedDate = reviewDto.CreatedDate
            };
        }

        public static ReviewDto CreateReviewRequestToReviewDto(CreateReviewRequest createReviewRequest, string userEmail)
        {
            return new ReviewDto
            {
                RecipeId = createReviewRequest.RecipeId,
                UserEmail = userEmail,
                Text = createReviewRequest.Text,
                CreatedDate = createReviewRequest.CreatedDate
            };
        }

        public static ReviewDto UpdateReviewRequestToReviewDto(UpdateReviewRequest updateReviewRequest)
        {
            return new ReviewDto
            {
                Id = updateReviewRequest.Id,
                RecipeId = updateReviewRequest.RecipeId,
                Text = updateReviewRequest.Text,
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
                UserEmail = recipe.User.Email
            };
        }

        public static Recipe RecipeDtoToRecipe(RecipeDto recipeDto, List<Category> categories, User user)
        {
            return new Recipe
            {
                Id = recipeDto.Id,
                Title = recipeDto.Title,
                UserId = user.Email,
                User = user,
                Description = recipeDto.Description,
                Ingredients = recipeDto.Ingredients.ToArray(),
                Instructions = recipeDto.Instructions,
                Duration = recipeDto.Duration,
                Categories = categories
            };
        }

        public static RecipeDto CreateRecipeRequestToRecipeDto(CreateRecipeRequest createRecipeRequest, string userEmail)
        {
            return new RecipeDto
            {
                Title = createRecipeRequest.Title,
                UserEmail = userEmail,
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
