using Pizza_Ordering.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.DTOs
{
    public class PizzaDto : BaseDto
    {
        public long BasePizzaId { get; set; }

        public long? UserId { get; set; }

        public string Name { get; set; }

        public List<IngredientDto> Ingredients { get; set; }

        public decimal Price { get; set; }

        public PizzaType PizzaType { get; set; }
    }
}
