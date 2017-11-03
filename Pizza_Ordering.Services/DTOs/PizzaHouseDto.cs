using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.DTOs
{
    public class PizzaHouseDto
    {
        public AddressDto Location { get; set; }

        public long Id { get; set; }

        public int Open { get; set; }

        public int Close { get; set; }

        public long ModeratorId { get; set; }

        public int Capacity { get; set; }

        public List<IngredientQuantityDto> InStock { get; set; }
    }
}
