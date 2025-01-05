using Ecommerce_Core.DTOs;
using Ecommerce_Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;

        public OrdersController(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpGet("get-all-orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepo.GetAllOrdersAsync();
            if (!orders.Any()) return NoContent();

            return Ok(orders);
        }

        [HttpGet("get-order-by-id/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);
            if (order == null) return NotFound("Order not found");

            return Ok(order);
        }

        [HttpPost("add-order")]
        public async Task<IActionResult> AddOrder([FromBody] OrderDto orderDto)
        {
            var result = await _orderRepo.AddOrderAsync(orderDto);
            return Ok(result);
        }

        [HttpPut("update-order-status/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string status)
        {
            var result = await _orderRepo.UpdateOrderStatusAsync(id, status);
            if (result == "Order not found") return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("delete-order/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderRepo.DeleteOrderAsync(id);
            if (result == "Order not found") return NotFound(result);

            return Ok(result);
        }
    }
}
