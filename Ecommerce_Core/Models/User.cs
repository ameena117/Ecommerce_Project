using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.Models
{
    public class User : IdentityUser<int>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public string Role { get; set; } // Admin, Seller, Buyer
        public DateTime CreatedAt { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

}
