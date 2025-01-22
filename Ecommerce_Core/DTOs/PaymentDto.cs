using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.DTOs
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public double Amount { get; set; }
    }
}
