using Ecommerce_Core.DTOs;
using Ecommerce_Core.Models;

namespace Ecommerce_Core.Interfaces
{
    public interface IProductRepo 
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<IEnumerable<ProductDto>> GetProductsByNameAsync(string name);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<String> AddProductAsync(ProductDto productDto);
        Task<String> UpdateProductAsync(ProductDto productDto, int id);
        Task<String> DeleteProductAsync(int id);
        Task<IEnumerable<ProductDto>> GetProductsBySellerAsync(int sellerId);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<ProductDto>> GetProductsBySubCategoryAsync(int subCategoryId);
    }
}
