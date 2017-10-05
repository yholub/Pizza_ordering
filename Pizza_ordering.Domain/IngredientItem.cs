using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_ordering.Domain
{
    public class IngredientItem
    {
        public int Id { get; set; }

        public Ingredient Ingredient { get; set; }

        public int Quantity { get; set; }
    }
}
