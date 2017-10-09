using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models.Ingredient
{
    public class IngredientModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Weight { get; set; }
    }
}