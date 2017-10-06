using Pizza_Ordering.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_ordering.Domain
{
    public class _PizzaRecipe
    {
        public int Id { get; set; }

        public virtual List<IngredientItem> Ingredients { get; set; }

        public PizzaType PizzaType { get; set; }
    }
}
