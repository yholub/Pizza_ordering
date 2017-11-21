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
        [JsonProperty(PropertyName = "id")]
        public long PizzaId { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Quantity { get; set; }

        public string Name { get; set; }

        public List<OrderItemIngredientModel> Ingredients { get; set; }
    }
}