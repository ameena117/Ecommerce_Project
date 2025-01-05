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
using System.Xml.Linq;

namespace Ecommerce_Infrastracture.Repositories
{
    public class SubCategoryRepo : ISubCategoryRepo
    {
        private readonly AppDbContext context;

        public SubCategoryRepo(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<string> AddSubCategoryAsync(SubCateoryDto subCateoryDto)
        {
            if (subCateoryDto == null)
            {
                return "No thing to add";
            }
            var category = await context.Categories.FindAsync(subCateoryDto.CategoryId);
            if (category == null) {
                return "Main Category Not Found";
            }
            var subCategory = new SubCategory
            {
                Name = subCateoryDto.Name,
                CategoryId = (int)subCateoryDto.CategoryId,
            };

            // Add to context
            await context.SubCategories.AddAsync(subCategory);
            await context.SaveChangesAsync(); // Ensure async call
            return "Sub Category added successfully";
        }

        public async Task<string> DeleteSubCategoryAsync(int id)
        {
            
            var subCategory = await context.SubCategories.FirstOrDefaultAsync(s => s.Id == id);
            if (subCategory == null)
            {
                return "Not Found"; // Return an empty list
            }
            context.SubCategories.Remove(subCategory);
            await context.SaveChangesAsync();
            return $"{subCategory.Name} deleted";
            
        }

        public async Task<List<SubCateoryDto>> GetSubCategoriesAsync()
        {
            var subCategories = await context.SubCategories.ToListAsync();

            if (subCategories == null || !subCategories.Any())
            {
                return new List<SubCateoryDto>(); // Return an empty list
            }

            // Map entities to DTOs
            var subCategoryDtos = subCategories.Select(subCategories => new SubCateoryDto()
            {
                Name = subCategories.Name,
                CategoryId = subCategories.CategoryId,
            }).ToList();

            return subCategoryDtos;
        }

        public async Task<List<SubCateoryDto>> GetSubCategoryByCatIdAsync(int catId)
        {
            var subCategories = await context.SubCategories.Where(s => s.CategoryId == catId).ToListAsync();
            if (subCategories == null || !subCategories.Any())
            {
                return new List<SubCateoryDto>(); // Return an empty list
            }
            return subCategories.Select(subCategory => new SubCateoryDto
            {
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId
            }).ToList();
        }

        public async Task<SubCateoryDto> GetSubCategoryByIdAsync(int id)
        {
            var subCategory = await context.SubCategories.FirstOrDefaultAsync(s => s.Id == id);
            if (subCategory == null)
            {
                return null; // Return an empty list
            }
            return new SubCateoryDto
            {
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId,
            };
        }

        public async Task<SubCateoryDto> GetSubCategoryByNameAsync(string name)
        {
            var subCategory = await context.SubCategories.FirstOrDefaultAsync(s => s.Name == name);
            if (subCategory == null)
            {
                return null; // Return an empty list
            }
            return new SubCateoryDto
            {
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId,
            };
        }

        public async Task<string> UpdateSubCategoryAsync(SubCateoryDto subCategoryDto, int id)
        {
            var subCategory = await context.SubCategories.FirstOrDefaultAsync(s => s.Id == id);
            if (subCategory == null)
            {
                return "Not Found"; // Return an empty list
            }
            var category = await context.Categories.FindAsync(subCategoryDto.CategoryId);
            if (category == null)
            {
                return "Main Category Not Found";
            }
            subCategory.Name = subCategoryDto.Name;
            subCategory.CategoryId = (int)subCategoryDto.CategoryId;
            await context.SaveChangesAsync();
            return "Updated Succesfully";
        }

    }
}
