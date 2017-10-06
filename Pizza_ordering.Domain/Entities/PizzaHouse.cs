using Pizza_ordering.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_ordering.Domain.Entities
{
    public class PizzaHouse: BaseEntity
    {
        [Required]
        public long AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Location { get; set; }

        public TimeSpan OpenTime { get; set; }

        public TimeSpan СloseTime { get; set; }

        public virtual List<Place> Places { get; set; }
    }
}
