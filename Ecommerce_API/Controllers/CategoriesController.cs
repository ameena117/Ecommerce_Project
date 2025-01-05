using Ecommerce_Core.DTOs;
using Ecommerce_Core.Interfaces;
using Ecommerce_Core.Models;
using Ecommerce_Infrastracture.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Ecommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoriesController(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            var result = await _categoryRepo.AddCategoryAsync(categoryDto);

            if (result == "Category added successfully")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("get-all-categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepo.GetCategoriesAsync();
            if (categories == null || !categories.Any())
                return NoContent();
            return Ok(categories);
        }

        [HttpGet("get-category-by-id")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(id);
            if (category == null)
                return NoContent();
            return Ok(category);
        }

        [HttpGet("get-category-by-Name")]
        public async Task<IActionResult> GetCategoryByName(string name)
        {
            var category = await _categoryRepo.GetCategoryByNameAsync(name);
            if (category == null)
                return NoContent();
            return Ok(category);
        }

        [HttpDelete("delete-category")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepo.DeleteCategoryAsync(id);
            if (category == null)
                return NoContent();
            return Ok(category);
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(CategoryDto categoryDto, int id)
        {
            var category = await _categoryRepo.UpdateCategoryAsync(categoryDto,id);
            if (category == "Not Found")
                return NoContent();
            return Ok(category);
        }
    }
}
