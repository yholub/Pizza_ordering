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

        public TimeSpan Open { get; set; }

        public TimeSpan Close { get; set; }
    }
}
