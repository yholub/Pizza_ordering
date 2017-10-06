using Pizza_ordering.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_ordering.Domain.Entities
{
    public class CapacityPlan: BaseEntity
    {
        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; set; }

        public TimeSpan TimeForOnePizza { get; set; }

        public int CountOfPizza { get; set; }
    }
}
