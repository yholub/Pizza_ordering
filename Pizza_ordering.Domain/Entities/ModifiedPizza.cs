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
    public class ModifiedPizza : BaseEntity
    {
        [Required]
        public long FixPizzaId { get; set; }

        [ForeignKey("FixPizzaId")]
        public virtual FixPizza BasePizza { get; set; }

        public virtual List<IngredientItem> IngredientItems { get; set; }

        // Should be introduced
        //public long? UserId { get; set; }
    }
}
