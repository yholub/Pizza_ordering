using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models.Pizza
{
    public class CreateSavedPizzaModel
    {
        public long BasePizzaId { get; set; }

        public string Name { get; set; }

        public List<IngredientDto> Ingredients { get; set; }

        public decimal Price { get; set; }
    }
}