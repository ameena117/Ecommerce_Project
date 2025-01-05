using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.DTOs
{
    public class CartDto
    {
        [Required(ErrorMessage ="User Id required")]
        public int UserId { get; set; }
    }
}
