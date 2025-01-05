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

namespace Ecommerce_Infrastracture.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext _context;

        public OrderRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ToListAsync();

            return orders.Select(o => new OrderDto
            {
                UserId = o.UserId,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                CreatedAt = o.CreatedAt,
                OrderProducts = o.OrderProducts.Select(op => new OrderProductDto
                {
                    ProductId = op.ProductId,
                    Quantity = op.Quantity,
                    Price = (double)op.Price,
                    
                }).ToList()
            }).ToList();
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders.Include(o => o.User).Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return null;

            return new OrderDto
            {
                UserId = order.UserId,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                OrderProducts = order.OrderProducts.Select(op => new OrderProductDto
                {
                    ProductId = op.ProductId,
                    Quantity = op.Quantity,
                    Price = (double)op.Price,
                }).ToList()
            };
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _context.Orders.Include(o => o.OrderProducts).Where(o => o.UserId == userId).ToListAsync();

            return orders.Select(o => new OrderDto
            {
                UserId = o.UserId,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                CreatedAt = o.CreatedAt,
                OrderProducts = o.OrderProducts.Select(op => new OrderProductDto
                {
                    ProductId = op.ProductId,
                    Quantity = op.Quantity,
                    Price = (double)op.Price,
                }).ToList()
            }).ToList();
        }

        public async Task<string> AddOrderAsync(OrderDto orderDto)
        {
            // First, get all the products related to the order
            var productIds = orderDto.OrderProducts.Select(op => op.ProductId).ToList();
            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            // Now, create the order and calculate the total price based on product prices
            var order = new Order
            {
                UserId = orderDto.UserId,
                Status = "New",
                CreatedAt = DateTime.UtcNow,
                // Calculate TotalPrice based on the quantity and the actual product price
                TotalPrice = orderDto.OrderProducts.Sum(op =>
                {
                    var product = products.FirstOrDefault(p => p.Id == op.ProductId);
                    if (product != null)
                    {
                        return op.Quantity * product.Price; // Multiply quantity by the product's price
                    }
                    return 0; // Return 0 if the product is not found
                }),
                OrderProducts = orderDto.OrderProducts.Select(op => new OrderProducts
                {
                    ProductId = op.ProductId,
                    Quantity = op.Quantity,
                    Price = products.FirstOrDefault(p => p.Id == op.ProductId)?.Price ?? 0, // Get the product's price
                }).ToList()
            };


            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return "Order added successfully";
        }

        public async Task<string> UpdateOrderStatusAsync(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return "Order not found";

            order.Status = status;
            await _context.SaveChangesAsync();

            return "Order status updated successfully";
        }

        public async Task<string> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return "Order not found";

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return "Order deleted successfully";
        }
    }
}
