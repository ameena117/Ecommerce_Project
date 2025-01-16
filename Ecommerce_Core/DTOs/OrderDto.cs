using Ecommerce_Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.DTOs
{
    public class OrderDto
    {
        [Required(ErrorMessage = "User Id required")]
        public int UserId { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderProductDto> OrderProducts { get; set; } // Nested DTO for related products
    }

}
