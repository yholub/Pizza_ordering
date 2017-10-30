using Pizza_Ordering.Common;
using Pizza_Ordering.Domain.Entities;
using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models.OrderViewModel
{
    public class OrderViewModel
    {
        public string StartStr { get; set; }

        public string EndStr { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public long Id { get; set; }

        public long OrderId { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public int StHour { get; set; }

        public int StMinute { get; set; }

        public int EndHour { get; set; }

        public int EndMinute { get; set; }

        public PizzaStatusType State { get; set; }

        public OrderViewModel(OrderItemDto ord)
        {
            Id = ord.Id;
            State = ord.Status;
            Start = ord.StartTime;
            End = ord.EndTime;
            OrderId = ord.OrderId;
            Price = ord.Price;
            Name = ord.PizzaName;
            StartStr = Start.ToShortTimeString();
            EndStr = End.ToShortTimeString();
            StHour = Start.Hour;
            StMinute = Start.Minute;
            EndHour = End.Hour;
            EndMinute = End.Minute;
        }
    }
}