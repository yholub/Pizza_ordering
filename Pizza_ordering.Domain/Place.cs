using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_ordering.Domain
{
    public class Place
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public virtual List<Pizza> Pizzas { get; set; }
    }
}