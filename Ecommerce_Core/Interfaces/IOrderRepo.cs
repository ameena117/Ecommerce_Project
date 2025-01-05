using Ecommerce_Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.Interfaces
{
        public interface IOrderRepo
        {
            Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
            Task<OrderDto> GetOrderByIdAsync(int id);
            Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId);
            Task<string> AddOrderAsync(OrderDto orderDto);
            Task<string> UpdateOrderStatusAsync(int id, string status);
            Task<string> DeleteOrderAsync(int id);
        }
}

