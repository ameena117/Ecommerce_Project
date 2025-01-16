using Ecommerce_Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.Interfaces
{
    public interface ICartRepo
    {
        Task<CartDto> GetCartByUserIdAsync(int userId);
        Task<string> AddProductToCartAsync(int userId, CartProductDto cartProductDto);
        Task<string> UpdateCartAsync(CartDto cartDto);
        Task<string> RemoveProductFromCartAsync(int userId, int productId);
        Task<string> ClearCartAsync(int userId);
    }
}
