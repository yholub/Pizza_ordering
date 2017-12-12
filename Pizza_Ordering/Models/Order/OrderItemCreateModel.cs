using Newtonsoft.Json;
using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models.Order
{
    public class OrderItemCreateModel
    {
        public long PizzaId { get; set; }

        public int Count { get; set; }

        public string Name { get; set; }

        public List<OrderItemIngredientModel> Ingredients { get; set; }
    }
}