﻿using Pizza_ordering.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_ordering.Domain.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Weight { get; set; }
    }
}
