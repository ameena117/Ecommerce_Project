using Ecommerce_Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_Core.DTOs
{
    public class ProductDto
    {
        [Required(ErrorMessage ="Product Name Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product Description Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Product Price Required")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Stock Required")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Category Id Required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Sub Category Id Required")]

        public int SubCategoryId { get; set; }
        [Required(ErrorMessage = "Seller Id Required")]
        public int SellerId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }

}