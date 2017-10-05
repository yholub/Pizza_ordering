using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models.Product
{
    public class CreateProductModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}