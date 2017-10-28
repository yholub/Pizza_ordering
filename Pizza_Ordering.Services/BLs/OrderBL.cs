using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizza_Ordering.DataProvider.UnitOfWork;
using Pizza_Ordering.Domain.Entities;

namespace Pizza_Ordering.Services.BLs
{
    public class OrderBL : BaseBL, IOrderBL
    {
        public OrderBL(IUnitOfWorkFactory factory)
            : base(factory)
        {
        }

        public void CreateOrderItem(OrderItemDto orderItemDto, long orderId)
        {
            UseDb(uow =>
            {
                var entity = new OrderItem
                {
                    PizzaId = orderItemDto.PizzaId,
                    IsModified = orderItemDto.IsModified,
                    StartTime = orderItemDto.StartTime,
                    EndTime = orderItemDto.EndTime,
                    OrderId = orderId,
                    Price = orderItemDto.Price
                };

                uow.OrderItems.Create(entity);

                uow.Save();

            });
        }

        public void CreateOrder(OrderDto orderDto)
        {
            UseDb(uow =>
            {
                var entity = new Domain.Entities.Order
                {
                    UserId = orderDto.UserId,
                    Price = orderDto.Price,
                    PizzaHouseId = orderDto.PizzaHouseId,
                    TimeToTake = orderDto.TimeToTake,
                    Status = orderDto.Status,
                    Items = orderDto.Items.Select(x => new OrderItem
                    {
                        PizzaId = x.PizzaId,
                        IsModified = x.IsModified,
                        StartTime = x.StartTime,
                        EndTime = x.EndTime,
                        Price = x.Price
                    }).ToList()
                };

                uow.Orders.Create(entity);

                uow.Save();
            });
        }

        public List<Pizza_Ordering.Services.DTOs.Order> GetOrdersSince(DateTime st)
        {
            throw new NotImplementedException();
        }

        public List<Pizza_Ordering.Services.DTOs.Order> GetPendingOrdersSince(DateTime st)
        {
            throw new NotImplementedException();
        }

        public void Reject(int id)
        {
            throw new NotImplementedException();
        }

        List<DTOs.Order> IOrderBL.GetOrdersSince(DateTime st)
        {
            throw new NotImplementedException();
        }

        List<DTOs.Order> IOrderBL.GetPendingOrdersSince(DateTime st)
        {
            throw new NotImplementedException();
        }

        public void Accept(int id)
        {
            throw new NotImplementedException();
        }
    }
}
