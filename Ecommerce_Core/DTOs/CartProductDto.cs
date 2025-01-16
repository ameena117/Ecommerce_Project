using Ecommerce_Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.DTOs
{
    public class CartProductDto
    {
        [Required(ErrorMessage = "Product Id required")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

