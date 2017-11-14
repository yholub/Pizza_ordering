using Pizza_Ordering.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models.Order
{
    public class CreateOrderModel
    {
        public decimal Price { get; set; }

        public string Name { get; set; }

        public PizzaStatusType Status { get; set; }

        public long? UserId { get; set; }

        public long PizzaHouseId { get; set; }

        public List<CreateOrderItemModel> Items { get; set; }
    }
}