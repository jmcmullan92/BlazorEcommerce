﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }

        public int ProductTypeId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
