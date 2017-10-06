using Pizza_Ordering.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_ordering.Domain
{
    public class _Pizza
    {
        public int Id { get; set; }

        public virtual _PizzaRecipe Recipe { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }
    }
}
