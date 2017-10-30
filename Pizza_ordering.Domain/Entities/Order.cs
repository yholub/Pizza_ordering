using Pizza_Ordering.Common;
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
    public class Order : BaseEntity
    {
        public virtual List<OrderItem> Items { get; set; }

        [Required]
        public decimal Price { get; set; }

        public long? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public long PizzaHouseId { get; set; }

        [ForeignKey("PizzaHouseId")]
        public virtual PizzaHouse PizzaHouse { get; set; }

        [Required]
        public PizzaStatusType Status { get; set; }

        [Required]
        public DateTime TimeToTake { get; set; }
    }
}