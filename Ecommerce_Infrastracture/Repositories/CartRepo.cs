using Ecommerce_Core.DTOs;
using Ecommerce_Core.Interfaces;
using Ecommerce_Core.Models;
using Ecommerce_Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Infrastracture.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly AppDbContext _context;

        public CartRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CartDto> GetCartByUserIdAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartProducts)
                .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) return null;

            return new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartProducts = cart.CartProducts.Select(cp => new CartProductDto
                {
                    ProductId = cp.ProductId,
                    Quantity = cp.Quantity,
                }).ToList()
            };
        }

        public async Task<string> AddProductToCartAsync(int userId, CartProductDto cartProductDto)
        {
            var cart = await _context.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartProducts = new List<CartProducts>()
                };
                _context.Carts.Add(cart);
            }

            var existingProduct = cart.CartProducts.FirstOrDefault(cp => cp.ProductId == cartProductDto.ProductId);
            if (existingProduct != null)
            {
                existingProduct.Quantity += cartProductDto.Quantity;
            }
            else
            {
                cart.CartProducts.Add(new CartProducts
                {
                    ProductId = cartProductDto.ProductId,
                    Quantity = cartProductDto.Quantity,
                    Price = (await _context.Products.FindAsync(cartProductDto.ProductId))?.Price ?? 0
                });
            }

            await _context.SaveChangesAsync();
            return "Product added to cart successfully";
        }
        public async Task<string> UpdateCartAsync(CartDto cartDto)
        {
            var cart = await _context.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefaultAsync(c => c.UserId == cartDto.UserId);

            if (cart == null)
                return "Cart not found for the user";

            foreach (var productDto in cartDto.CartProducts)
            {
                var existingProduct = cart.CartProducts.FirstOrDefault(cp => cp.ProductId == productDto.ProductId);

                if (existingProduct != null)
                {
                    existingProduct.Quantity = productDto.Quantity;
                    existingProduct.Price = productDto.Quantity * (await _context.Products
                        .Where(p => p.Id == productDto.ProductId)
                        .Select(p => p.Price)
                        .FirstOrDefaultAsync());
                }
                else
                {
                    cart.CartProducts.Add(new CartProducts
                    {
                        ProductId = productDto.ProductId,
                        Quantity = productDto.Quantity,
                        Price = productDto.Quantity * (await _context.Products
                            .Where(p => p.Id == productDto.ProductId)
                            .Select(p => p.Price)
                            .FirstOrDefaultAsync())
                    });
                }
            }

            await _context.SaveChangesAsync();
            return "Cart updated successfully";
        }
        public async Task<string> RemoveProductFromCartAsync(int userId, int productId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) return "Cart not found";

            var product = cart.CartProducts.FirstOrDefault(cp => cp.ProductId == productId);
            if (product == null) return "Product not found in cart";

            cart.CartProducts.Remove(product);
            await _context.SaveChangesAsync();

            return "Product removed from cart successfully";
        }

        public async Task<string> ClearCartAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) return "Cart not found";

            cart.CartProducts.Clear();
            await _context.SaveChangesAsync();

            return "Cart cleared successfully";
        }
    }
}
