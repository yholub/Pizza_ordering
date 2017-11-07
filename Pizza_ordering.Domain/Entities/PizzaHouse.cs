using Pizza_Ordering.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Domain.Entities
{
    public class PizzaHouse : BaseEntity
    {
        [Required]
        public long AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Location { get; set; }

        public virtual List<CapacityPlan> CapacityPlans { get; set; }

        public TimeSpan OpenTime { get; set; }

        public TimeSpan CloseTime { get; set; }

        public long ModeratorId { get; set; }

        public int Capacity { get; set; }

        [ForeignKey("ModeratorId")]
        public virtual User Moderator { get; set; }

        public virtual List<IngredientAmount> InStock { get; set; }
    }
}
