using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_Ordering.Models.Pizza
{
    public class CreateModifiedPizzaModel
    {
        public long UserId { get; set; }

        public long BasePizzaId { get; set; }

        public List<IngredientDto> Ingredients { get; set; }
    }
}