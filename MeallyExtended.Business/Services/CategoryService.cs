using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.Contracts.Dto;
using MeallyExtended.DataModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Net.Mail;
using MeallyExtended.Contracts.Requests.Category;
using Microsoft.EntityFrameworkCore;

namespace MeallyExtended.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category?> GetCategoryByName(string name)
        {
            return await _categoryRepository.GetCategoryByName(name);
        }
    }
}
