using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models.Ingredient
{
    public class CreateIngredientModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Weight { get; set; }
    }
}