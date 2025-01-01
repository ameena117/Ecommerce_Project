using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.Models
{
    public class Payment
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime PaymentDate { get; set; }
        public String PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
}
