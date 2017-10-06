using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_ordering.Domain
{
    public class _Order
    {
        public int Id { get; set; }

        public List<_Pizza> Pizzas { get; set; }

        public decimal Price { get; set; }

        public DateTime Time { get; set; }
    }
}
