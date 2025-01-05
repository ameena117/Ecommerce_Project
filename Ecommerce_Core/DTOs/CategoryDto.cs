using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.DTOs
{
    public class CategoryDto
    {
        [Required(ErrorMessage = "Category name is required")]
        public String Name { get; set; }
    }
}
