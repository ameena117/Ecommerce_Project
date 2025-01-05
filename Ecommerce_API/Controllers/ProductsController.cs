using Ecommerce_Core.DTOs;
using Ecommerce_Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _productRepo;

        public ProductsController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            var result = await _productRepo.AddProductAsync(productDto);
            if (result != "Product added Succesfully")
            {
                return BadRequest(result);
            }
            return Ok("Product added successfully");
        }

        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepo.GetAllProductsAsync();
            if (products == null || !products.Any())
                return NoContent();
            return Ok(products);
        }

        [HttpGet("get-product-by-id")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepo.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Product not found");
            return Ok(product);
        }

        [HttpGet("get-products-by-name")]
        public async Task<IActionResult> GetProductsByName(string name)
        {
            var products = await _productRepo.GetProductsByNameAsync(name);
            if (products == null || !products.Any())
                return NotFound("No products found with the specified name");
            return Ok(products);
        }

        [HttpGet("get-products-by-seller")]
        public async Task<IActionResult> GetProductsBySeller(int sellerId)
        {
            var products = await _productRepo.GetProductsBySellerAsync(sellerId);
            if (products == null || !products.Any())
                return NotFound("No products found for the specified seller");
            return Ok(products);
        }

        [HttpGet("get-products-by-category")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productRepo.GetProductsByCategoryAsync(categoryId);
            if (products == null || !products.Any())
                return NotFound("No products found for the specified category");
            return Ok(products);
        }

        [HttpGet("get-products-by-sub-category")]
        public async Task<IActionResult> GetProductsBySubCategory(int subCategoryId)
        {
            var products = await _productRepo.GetProductsBySubCategoryAsync(subCategoryId);
            if (products == null || !products.Any())
                return NotFound("No products found for the specified sub-category");
            return Ok(products);
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto, int id)
        {
            var result = await _productRepo.UpdateProductAsync(productDto, id);
            if (result == "Not Found")
                return NotFound("Product not found");
            return Ok("Product updated successfully");
        }

        [HttpDelete("delete-product")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productRepo.DeleteProductAsync(id);
            if (result == "Not Found")
                return NotFound("Product not found");
            return Ok(result);
        }
    }

}
