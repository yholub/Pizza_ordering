﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.DTOs
{
    public enum State
    {
        Pending, 
        Accept, 
        Rejected
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Price { get; set; }

        public string Name { get; set; }

        public State State { get; set; }
    }
}
