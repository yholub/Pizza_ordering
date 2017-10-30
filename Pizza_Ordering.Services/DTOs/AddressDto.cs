using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.DTOs
{
    public class AddressDto
    {
        public double Lat { get; set; }

        public double Lon { get; set; }

        public string City { get; set; }

        public string StreetName { get; set; }

        public string HouseNumber { get; set; }
    }
}
