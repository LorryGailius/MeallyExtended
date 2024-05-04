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
                cfg.CreateMap<Ingredient, IngredientDto>();

                cfg.CreateMap<Review, ReviewDto>()
               .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));




                cfg.CreateMap<Recipe, RecipeDto>()
                 .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)))
                 .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                 .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.RecipeLikes.LikeCount))
                 .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                 .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));


            });

            _mapper = configuration.CreateMapper();
        }

        public IngredientDto MapIngredientToDto(Ingredient ingredient)
        {
            return _mapper.Map<IngredientDto>(ingredient);
        }

        public ReviewDto MapReviewToDto(Review review)
        {
            return _mapper.Map<ReviewDto>(review);
        }

        public RecipeDto MapRecipeToDto(Recipe recipe)
        {
            return _mapper.Map<RecipeDto>(recipe);
        }
    }

}
