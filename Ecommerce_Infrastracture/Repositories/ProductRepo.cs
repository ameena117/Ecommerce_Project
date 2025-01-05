using Ecommerce_Core.DTOs;
using Ecommerce_Core.Interfaces;
using Ecommerce_Core.Mapping_Profiles;
using Ecommerce_Core.Models;
using Ecommerce_Infrastracture.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ecommerce_Infrastracture.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;

        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsBySellerAsync(int sellerId)
        {
            var products = await _context.Products
                .Where(p => p.SellerId == sellerId).ToListAsync();
            if (products.Any())
            {
                return products.Select(p => new ProductDto()
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    CreatedAt = p.CreatedAt,
                    LastUpdatedAt = (p.LastUpdatedAt != null) ? (DateTime)p.LastUpdatedAt : DateTime.MinValue,
                    CategoryId = p.CategoryId,
                    SubCategoryId = p.SubCategoryId,
                    SellerId = p.SellerId,
                }).ToList();
            }
            return null;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _context.Products
                .Where(p => p.CategoryId == categoryId).ToListAsync();
            if (products.Any())
            {
                return products.Select(p => new ProductDto()
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    CreatedAt = p.CreatedAt,
                    LastUpdatedAt = (p.LastUpdatedAt != null) ? (DateTime)p.LastUpdatedAt : DateTime.MinValue,
                    CategoryId = p.CategoryId,
                    SubCategoryId = p.SubCategoryId,
                    SellerId = p.SellerId,
                }).ToList();
            }
            return null;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsBySubCategoryAsync(int subCategoryId)
        {
            var products = await _context.Products
                .Where(p => p.SubCategoryId == subCategoryId).ToListAsync();
            if (products.Any())
            {
                return products.Select(p => new ProductDto()
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    CreatedAt = p.CreatedAt,
                    LastUpdatedAt = (p.LastUpdatedAt != null) ? (DateTime)p.LastUpdatedAt : DateTime.MinValue,
                    CategoryId = p.CategoryId,
                    SubCategoryId = p.SubCategoryId,
                    SellerId = p.SellerId,
                }).ToList();
            }
            return null;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            // Map entities to DTOs
            return products.Select(p => new ProductDto()
            {       
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                CreatedAt = p.CreatedAt,
                LastUpdatedAt = (p.LastUpdatedAt != null) ? (DateTime)p.LastUpdatedAt : DateTime.MinValue,
                CategoryId = p.CategoryId,
                SubCategoryId = p.SubCategoryId,
                SellerId = p.SellerId,
            }).ToList();
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return null;
            return new ProductDto()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt,
                LastUpdatedAt = (product.LastUpdatedAt != null) ? 
                (DateTime)product.LastUpdatedAt:
                DateTime.MinValue,
                CategoryId = product.CategoryId,
                SubCategoryId = product.SubCategoryId,
                SellerId = product.SellerId,
            };
        }

        public async Task<String> AddProductAsync(ProductDto productDto)
        {
            if (productDto == null)
                return "Not Found";
            var category = await _context.Categories.FindAsync(productDto.CategoryId);
            if (category == null)
            {
                return "Main Category Not Found";
            }
            var subCategory = await _context.SubCategories.FindAsync(productDto.SubCategoryId);
            if (subCategory == null)
            {
                return "Sub Category Not Found";
            }
            var seller = await _context.Users.FindAsync(productDto.SellerId);
            if (seller == null)
            {
                return "Seller Not Found";
            }
            else
            {
                var product = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Stock = productDto.Stock,
                    CategoryId = productDto.CategoryId,
                    SubCategoryId = productDto.SubCategoryId,
                    SellerId = productDto.SellerId,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = null,
                };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return "Product added Succesfully";
            }
        }

        public async Task<String> UpdateProductAsync(ProductDto productDto, int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return "Not Found";
            }

            {
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;
                product.Stock = productDto.Stock;
                product.CategoryId = productDto.CategoryId;
                product.SubCategoryId = productDto.SubCategoryId;
                product.SellerId = productDto.SellerId;
                product.CreatedAt = productDto.CreatedAt;
                product.LastUpdatedAt = DateTime.UtcNow;
            };

            await _context.SaveChangesAsync();
            return "Updated Succesfully";
        }

        public async Task<string> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return "Not Found";
            _context.Products.Remove(product);
            _context.SaveChanges();
            return $"{product.Name} deleted";
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByNameAsync(string name)
        {
            var products = await _context.Products
                .Where(p => p.Name == name).ToListAsync();
            if (products.Any())
            {
                return products.Select(p => new ProductDto()
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    CreatedAt = p.CreatedAt,
                    LastUpdatedAt = (p.LastUpdatedAt != null) ? (DateTime)p.LastUpdatedAt : DateTime.MinValue,
                    CategoryId = p.CategoryId,
                    SubCategoryId = p.SubCategoryId,
                    SellerId = p.SellerId,
                }).ToList();
            }
            return null;
        }

    }

}
