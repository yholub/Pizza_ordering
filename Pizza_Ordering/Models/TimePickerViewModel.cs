using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models
{
    public class PizzaHouseTimePickerViewModel
    {
        public long PizzaHouseId { get; set;}
        public Dictionary<int, Dictionary<int, bool>> Time { get; set; }
    }
}