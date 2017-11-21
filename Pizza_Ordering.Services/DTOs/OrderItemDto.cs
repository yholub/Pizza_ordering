using Pizza_Ordering.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.DTOs
{
    public class OrderItemDto
    {
        public long Id { get; set; }

        public long PizzaId { get; set; }

        public string PizzaName { get; set; }

        public bool IsModified { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal Price { get; set; }

        public long OrderId { get; set; }

        public PizzaStatusType Status { get; set; }

        public int Quantity { get; set; }
    }
}
