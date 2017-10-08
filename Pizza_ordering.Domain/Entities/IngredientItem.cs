using Pizza_Ordering.Domain.Abstract;
using Pizza_Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Domain.Entities
{
    public class IngredientItem : BaseEntity
    {
        [Required]
        public long IngredientId { get; set; }

        [ForeignKey("IngredientId")]
        public virtual Ingredient Ingredient { get; set; }

        public int Quantity { get; set; }
    }
}
