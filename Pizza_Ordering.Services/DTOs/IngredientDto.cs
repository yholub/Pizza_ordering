using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.DTOs
{
    public class IngredientDto : BaseDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Weight { get; set; }

        public int Quantity { get; set; }

        public bool IsLocked { get; set; }
    }
}
