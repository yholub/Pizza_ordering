using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models.Product
{
    public class ProductModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}