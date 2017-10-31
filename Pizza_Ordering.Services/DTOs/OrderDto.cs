using Pizza_Ordering.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.DTOs
{
    public class OrderDto : BaseDto
    {
        public decimal Price { get; set; }

        public string Name { get; set; }

        public PizzaStatusType Status { get; set; }

        public long? UserId { get; set; }

        public long PizzaHouseId { get; set; }

        public List<OrderItemDto> Items { get; set; }

        public DateTime TimeToTake { get; set; }
    }
}