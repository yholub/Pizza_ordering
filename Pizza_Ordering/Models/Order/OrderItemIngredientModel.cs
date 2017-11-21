using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models.Order
{
    public class OrderItemIngredientModel
    {
        public long Id { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Quantity { get; set; }
    }
}