using Pizza_Ordering.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Domain.Entities
{
    public class FixPizza : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual List<IngredientItem> IngredientItems { get; set; }
    }
}
