using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Domain.Abstract
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; private set; }

        public BaseEntity()
        {
        }
    }
}
