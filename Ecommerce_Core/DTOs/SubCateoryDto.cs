using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.DTOs
{
    public class SubCateoryDto
    {
        [Required(ErrorMessage = "Category Id required")]
        public int? CategoryId { get; set; }  

        [Required(ErrorMessage = "Sub Category name is required")]
        public String Name { get; set; }
    }
}
