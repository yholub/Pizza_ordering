using Pizza_Ordering.Domain.Entities;
using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.Interfaces
{
    public interface IOrderBL
    {
        List<OrderItemDto> GetOrderItemsSince(DateTime st, long houseId, bool onlyPending = false);

       void Accept(int id);

       void Reject(int id);

       void CreateOrderItem(OrderItemDto orderItemDto, long orderId);

       void CreateOrder(OrderDto orderDto);
    }
}
