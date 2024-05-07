﻿using AutoMapper;
using MeallyExtended.Contracts.Dto;
using MeallyExtended.Contracts.Requests.Recipe;
using MeallyExtended.DataModels.Entities;

namespace MeallyExtended.Business.Mappers
{
    public interface IMeallyMapper
    {
        CategoryDto MapCategoryToDto(Category category);
        Category MapCategoryToEntity(CategoryDto categoryDto);
        Ingredient MapIngredientToEntity(IngredientDto ingredientDto);
        IngredientDto MapIngredientToDto(Ingredient ingredient);
        ReviewDto MapReviewToDto(Review review);
        Review MapReviewToEntity(ReviewDto reviewDto);
        RecipeDto MapRecipeToDto(Recipe recipe);
        Recipe MapRecipeDtoToEntity(RecipeDto recipeDto);
        RecipeDto MapCreateRecipeRequestToDto(CreateRecipeRequest request);
        RecipeDto MapUpdateRecipeRequestToDto(UpdateRecipeRequest request);
    }

    public class MeallyMapper : IMeallyMapper
    {
        private readonly IMapper _mapper;

        public MeallyMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<CategoryDto, Category>();
                cfg.CreateMap<Ingredient, IngredientDto>();
                cfg.CreateMap<IngredientDto, Ingredient>();

                cfg.CreateMap<Review, ReviewDto>()
               .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));
                cfg.CreateMap<ReviewDto, Review>()
               .ForMember(dest => dest.User.Email, opt => opt.MapFrom(src => src.UserEmail));


                cfg.CreateMap<Recipe, RecipeDto>()
                 .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)))
                 .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                 .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.RecipeLikes.LikeCount))
                 .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                 .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));

                cfg.CreateMap<RecipeDto, Recipe>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore())  // Assuming Category cannot be simply reconstructed
                .ForMember(dest => dest.User, opt => opt.Ignore())        // Assuming User cannot be simply reconstructed
                .ForMember(dest => dest.RecipeLikes, opt => opt.Ignore()) // Assuming RecipeLikes cannot be reconstructed directly
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));

                cfg.CreateMap<CreateRecipeRequest, RecipeDto>();
                cfg.CreateMap<UpdateRecipeRequest, RecipeDto>();
            });

            _mapper = configuration.CreateMapper();
        }

        public CategoryDto MapCategoryToDto(Category category)
        {
            return _mapper.Map<CategoryDto>(category);
        }

        public Category MapCategoryToEntity(CategoryDto categoryDto)
        {
            return _mapper.Map<Category>(categoryDto);
        }

        public Ingredient MapIngredientToEntity(IngredientDto ingredientDto)
        {
            return _mapper.Map<Ingredient>(ingredientDto);
        }

        public IngredientDto MapIngredientToDto(Ingredient ingredient)
        {
            return _mapper.Map<IngredientDto>(ingredient);
        }

        public ReviewDto MapReviewToDto(Review review)
        {
            return _mapper.Map<ReviewDto>(review);
        }

        public Review MapReviewToEntity(ReviewDto reviewDto)
        {
            return _mapper.Map<Review>(reviewDto);
        }

        public RecipeDto MapRecipeToDto(Recipe recipe)
        {
            return _mapper.Map<RecipeDto>(recipe);
        }

        public Recipe MapRecipeDtoToEntity(RecipeDto recipeDto)
        {
            return _mapper.Map<Recipe>(recipeDto);
        }

        public RecipeDto MapCreateRecipeRequestToDto(CreateRecipeRequest request)
        {
            return _mapper.Map<RecipeDto>(request);
        }

        public RecipeDto MapUpdateRecipeRequestToDto(UpdateRecipeRequest request)
        {
            return _mapper.Map<RecipeDto>(request);
        }
    }
}
