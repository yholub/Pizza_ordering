using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models
{
    public class PizzaHouseTimeViewModel
    {
        public PizzaHouseTimeViewModel()
        {
            Time = new Dictionary<int, Dictionary<int, bool>>();
            Hours = new Dictionary<int, bool>();
        }

        public void Set(int hour, int minute)
        {
            if (!Time.ContainsKey(hour))
            {
                Time[hour] = new Dictionary<int, bool>();
            }

            Time[hour][minute] = true;
            Hours[hour] = true;
        }
        public long PizzaHouseId { get; set; }

        public Dictionary<int, Dictionary<int, bool>> Time { get; set; }

        public Dictionary<int, bool> Hours { get; set; }

    }
}