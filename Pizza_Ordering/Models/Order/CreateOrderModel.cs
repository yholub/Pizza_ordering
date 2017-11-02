using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models.Order
{
    public class CreateOrderModel
    {
        //public DateTime Start { get; set; }

        //public DateTime End { get; set; }

        public string Name { get; set; }

        public long? UserId { get; set; }

        public long PizzaHouseId { get; set; }

        public List<CreateOrderItemModel> Items { get; set; }

        //public DateTime TimeToTake { get; set; }
    }
}