using Pizza_Ordering.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Domain.Entities
{
    public class Address : BaseEntity
    {
        [Required]
        public string City { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string HouseNumber { get; set; }

        [Required]
        public double Lng { get; set; }

        [Required]
        public double Lat { get; set; }
    }
}
