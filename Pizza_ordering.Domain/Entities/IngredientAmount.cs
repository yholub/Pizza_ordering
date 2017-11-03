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
    public class IngredientAmount: BaseEntity
    {
        [Required]
        public long IngredientId { get; set; }

        [Required]
        public long PizzaHouseId { get; set; }

        [ForeignKey("IngredientId")]
        public virtual Ingredient Ingredient { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("PizzaHouseId")]
        public virtual PizzaHouse House { get; set; }
    }
}
