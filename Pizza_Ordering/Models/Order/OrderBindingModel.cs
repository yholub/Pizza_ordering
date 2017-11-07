using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models.Order
{
    public class OrderBindingModel
    {
        public string Time { get; set; }

        public long PizzaId { get; set; }

        public List<OrderItemBindingModel> OrderItems { get; set; }

        public OrderBindingModel()
        {
            OrderItems = new List<OrderItemBindingModel>();
        }
    }
}