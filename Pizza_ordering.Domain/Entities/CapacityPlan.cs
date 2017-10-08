using Pizza_Ordering.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Domain.Entities
{
    public class CapacityPlan : BaseEntity
    {
        [Required]
        public long PizzaHouseId { get; set; }

        [Required]
        public DateTime FromTime { get; set; }

        [Required]
        public DateTime ToTime { get; set; }

        [Required]
        public TimeSpan TimeForOnePizza { get; set; }

        [Required]
        public int CountOfPizza { get; set; }
    }
}
