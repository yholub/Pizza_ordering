using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.DTOs
{
    public class IngredientQuantityDto : BaseDto
    {
        public int Quantity { get; set; }

        public IngredientDto IngredientDto { get; set; }
    }
}
