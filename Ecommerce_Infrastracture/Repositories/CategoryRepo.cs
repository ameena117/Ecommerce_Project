using Ecommerce_Core.DTOs;
using Ecommerce_Core.Interfaces;
using Ecommerce_Core.Models;
using Ecommerce_Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Infrastracture.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext context;

        public CategoryRepo(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<string> AddCategoryAsync(CategoryDto categoryDto)
        {
            if(categoryDto == null)
            {
                return "No thing to add";
            }
            var category = new Category()
            {
                Name = categoryDto.Name,
            };

            // Add to context
            context.Categories.Add(category);
            await context.SaveChangesAsync(); // Ensure async call
            return "Category added successfully";
        }

        public async Task<string> DeleteCategoryAsync(int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return "Not Found"; // Return an empty list
            }
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return $"{category.Name} deleted";
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await context.Categories.ToListAsync();

            if (categories == null || !categories.Any())
            {
                return new List<CategoryDto>(); // Return an empty list
            }

            // Map entities to DTOs
            var categoryDtos = categories.Select(category => new CategoryDto
            {
                Name = category.Name
            }).ToList();

            return categoryDtos;
        }


        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) {
                return null; // Return an empty list
            }
            return new CategoryDto
            {
                Name = category.Name
            };
        }

        public async Task<CategoryDto> GetCategoryByNameAsync(string name)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Name == name);
            if (category == null)
            {
                return null; // Return an empty list
            }
            return new CategoryDto
            {
                Name = category.Name
            };
        }

        public async Task<string> UpdateCategoryAsync(CategoryDto categoryDto, int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return "Not Found"; // Return an empty list
            }
            category.Name = categoryDto.Name;
            await context.SaveChangesAsync();
            return "Updated Succesfully";

        }
    }
}
