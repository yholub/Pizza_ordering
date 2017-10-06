﻿using Pizza_ordering.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_ordering.Domain.Entities
{
    public class FixPizza: BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual List<Ingredient> Ingredients { get; set; }
    }
}
