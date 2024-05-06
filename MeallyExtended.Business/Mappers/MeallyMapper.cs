using AutoMapper;
using MeallyExtended.Contracts.Dto;
using MeallyExtended.DataModels.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeallyExtended.Business.Mappers
{
    public class MeallyMapper
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

        public Recipe MapRecipeToEntity(RecipeDto recipeDto)
        {
            return _mapper.Map<Recipe>(recipeDto);
        }
    }

}
