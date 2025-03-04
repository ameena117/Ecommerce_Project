﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.DTOs
{
    public class CartDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="User Id required")]
        public int UserId { get; set; }
        public List<CartProductDto> CartProducts { get; set; }
    }
}
