using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models.Order
{
    public class OrderItemBindingModel
    {
        public long PizzaBaseId { get; set; }

        public string PizzaName { get; set; }

        public int Count { get; set; }

        public List<IngredientBindingModel> Ingredients { get; set; }

        public OrderItemBindingModel()
        {
            Ingredients = new List<IngredientBindingModel>();
        }
    }
}