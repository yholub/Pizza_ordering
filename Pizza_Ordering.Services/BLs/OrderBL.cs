﻿using Pizza_Ordering.DataProvider.UnitOfWork;
using Pizza_Ordering.Domain.Entities;
using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Pizza_Ordering.Services.DTOs.OrderItemDto> GetOrderItemsSince(DateTime st, bool onlyPending = false)
        {
            List<OrderItemDto> orders = UseDb(db =>
            {
                var queryFix =
                    from ordItem in db.OrderItems.Query()
                    where ordItem.EndTime > st && !ordItem.IsModified && (!onlyPending || ordItem.Order.Status == Common.PizzaStatusType.Processed)
                    join pizza in db.FixPizzas.Query() on ordItem.PizzaId equals pizza.Id
                    select new OrderItemDto
                    {
                        Id = ordItem.Id,
                        PizzaName = pizza.Name,
                        Price = ordItem.Price,
                        PizzaId = pizza.Id,
                        StartTime = ordItem.StartTime,
                        EndTime = ordItem.EndTime,
                        OrderId = ordItem.OrderId,
                        Status = ordItem.Order.Status
                    };

                var queryMod =
                   from ordItem in db.OrderItems.Query()
                   where ordItem.EndTime < st && ordItem.IsModified
                   join pizza in db.FixPizzas.Query() on ordItem.PizzaId equals pizza.Id
                   select new OrderItemDto
                   {
                       Id = ordItem.Id,
                       OrderId = ordItem.OrderId,
                       PizzaName = "Custom",
                       Price = ordItem.Price,
                       PizzaId = pizza.Id,
                       StartTime = ordItem.StartTime,
                       EndTime = ordItem.EndTime,
                       Status = ordItem.Order.Status
                   };

                return queryFix.ToList().Concat(queryMod).ToList();
            });

            return orders;
        }

        public void Reject(int id)
        {
            UseDb(db =>
            {
                var entity = db.Orders.GetById(id);
                entity.Status = Common.PizzaStatusType.Refused;
                db.Save();
            });
        }

        public void Accept(int id)
        {
            UseDb(db =>
            {
                var entity = db.Orders.GetById(id);
                if (entity.Status == Common.PizzaStatusType.Processed)
                {
                    entity.Status = Common.PizzaStatusType.Refused;
                    db.Save();
                }
            });
        }
    }
}
