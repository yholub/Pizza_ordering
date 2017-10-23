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
    public class SavedPizza : BaseEntity
    {
        [Required]
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public string Name { get; set; }

        [Required]
        public long FixPizzaId { get; set; }

        [ForeignKey("FixPizzaId")]
        public virtual FixPizza BasePizza { get; set; }

        public virtual List<IngredientItem> IngredientItems { get; set; }
    }
}
