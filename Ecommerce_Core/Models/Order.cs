using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; } // New, In Progress, Completed
        public DateTime CreatedAt { get; set; }
        public ICollection<OrderProducts> OrderProducts { get; set; }
    }

}
