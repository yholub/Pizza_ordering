using Newtonsoft.Json;
using Pizza_Ordering.Common;
using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models.Order
{
    public class OrderCreateModel
    {
        public long PizzaHouseId { get; set; }

        public DateTime TimeToTake { get; set; }

        public List<OrderItemCreateModel> Items { get; set; }
    }
}