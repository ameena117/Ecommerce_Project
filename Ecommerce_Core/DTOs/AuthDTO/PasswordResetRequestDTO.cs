using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.DTOs.AuthDTO
{
    public class PasswordResetRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
