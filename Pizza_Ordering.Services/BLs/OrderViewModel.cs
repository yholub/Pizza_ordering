using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Services.BLs
{
    public class OrderViewModel
    {
        public string StartStr { get; set; }
        public string EndStr { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public int StHour { get; set; }
        public int StMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
        public State State { get; set; }
        public OrderViewModel(Order ord)
        {
            State = ord.State;
            Start = ord.Start;
            End = ord.End;
            Id = ord.Id;
            Price = ord.Price;
            Name = ord.Name;
            StartStr = Start.ToShortTimeString();
            EndStr = End.ToShortTimeString();
            StHour = Start.Hour;
            StMinute = Start.Minute;
            EndHour = End.Hour;
            EndMinute = End.Minute;
            
        }
    }
}