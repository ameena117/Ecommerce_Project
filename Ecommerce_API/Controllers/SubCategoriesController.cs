using Ecommerce_Core.DTOs;
using Ecommerce_Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryRepo _subCategoryRepo;

        public SubCategoriesController(ISubCategoryRepo subCategoryRepo)
        {
            _subCategoryRepo = subCategoryRepo;
        }

        [HttpPost("add-sub-category")]
        public async Task<IActionResult> AddSubCategory([FromBody] SubCateoryDto subCategoryDto)
        {
            var result = await _subCategoryRepo.AddSubCategoryAsync(subCategoryDto);

            if (result == "Sub Category added successfully")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("get-all-sub-categories")]
        public async Task<IActionResult> GetSubAllCategories()
        {
            var subCategories = await _subCategoryRepo.GetSubCategoriesAsync();
            if (subCategories == null || !subCategories.Any())
                return NoContent();
            return Ok(subCategories);
        }

        [HttpGet("get-sub-category-by-id")]
        public async Task<IActionResult> GetSubCategoryById(int id)
        {
            var subCategory = await _subCategoryRepo.GetSubCategoryByIdAsync(id);
            if (subCategory == null)
                return NoContent();
            return Ok(subCategory);
        }
        
        [HttpGet("get-sub-category-by-category-id")]
        public async Task<IActionResult> GetSubCategoryByCatId(int catId)
        {
            var subCategory = await _subCategoryRepo.GetSubCategoryByCatIdAsync(catId);
            if (subCategory == null)
                return NotFound();
            return Ok(subCategory);
        }

        [HttpGet("get-sub-category-by-Name")]
        public async Task<IActionResult> GetSubCategoryByName(string name)
        {
            var subCategory = await _subCategoryRepo.GetSubCategoryByNameAsync(name);
            if (subCategory == null)
                return NotFound();
            return Ok(subCategory);
        }

        [HttpDelete("delete-sub-category")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var subCategory = await _subCategoryRepo.DeleteSubCategoryAsync(id);
            if (subCategory == null)
                return NotFound();
            return Ok(subCategory);
        }

        [HttpPut("update-sub-category")]
        public async Task<IActionResult> UpdateCategory(SubCateoryDto subCategoryDto, int id)
        {
            var category = await _subCategoryRepo.UpdateSubCategoryAsync(subCategoryDto, id);
            if (category == "Not Found")
                return NotFound();
            return Ok(category);
        }
    }
}
