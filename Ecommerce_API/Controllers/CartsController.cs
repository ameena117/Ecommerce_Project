using Ecommerce_Core.DTOs;
using Ecommerce_Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepo _cartRepo;

        public CartsController(ICartRepo cartRepo)
        {
            _cartRepo = cartRepo;
        }

        [HttpGet("get-cart-by-user/{userId}")]
        public async Task<IActionResult> GetCartByUser(int userId)
        {
            var cart = await _cartRepo.GetCartByUserIdAsync(userId);
            if (cart == null) return NotFound("Cart not found");

            return Ok(cart);
        }

        [HttpPost("add-product/{userId}")]
        public async Task<IActionResult> AddProductToCart(int userId, [FromBody] CartProductDto cartProductDto)
        {
            var result = await _cartRepo.AddProductToCartAsync(userId, cartProductDto);
            return Ok(result);
        }
        [HttpPut("update-cart")]
        public async Task<IActionResult> UpdateCart([FromBody] CartDto cartDto)
        {
            var result = await _cartRepo.UpdateCartAsync(cartDto);
            if (result == "Cart not found for the user")
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("remove-product/{userId}/{productId}")]
        public async Task<IActionResult> RemoveProductFromCart(int userId, int productId)
        {
            var result = await _cartRepo.RemoveProductFromCartAsync(userId, productId);
            if (result == "Cart not found" || result == "Product not found in cart") return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("clear-cart/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var result = await _cartRepo.ClearCartAsync(userId);
            if (result == "Cart not found") return NotFound(result);

            return Ok(result);
        }
    }
}
