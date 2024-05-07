using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.Contracts.Dto;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MeallyExtended.Business.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMeallyMapper _mapper;

        public RecipeService(IRecipeRepository recipeRepository, IMeallyMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public async Task<Recipe> AddRecipe(RecipeDto recipe)
        {
            var recipeEntity = _mapper.MapRecipeDtoToEntity(recipe);

            await _recipeRepository.AddRecipe(recipeEntity);

            return recipeEntity;
        }

        public bool DeleteRecipe(Guid recipeId)
        {
            throw new NotImplementedException();
        }

        public RecipeDto GetRecipeById(Guid recipeId)
        {
            throw new NotImplementedException();
        }

        public RecipeDto GetRecipeByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RecipeDto> GetRecipesByCategory(List<CategoryDto> categories)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRecipe(RecipeDto recipe)
        {
            throw new NotImplementedException();
        }
    }
}
