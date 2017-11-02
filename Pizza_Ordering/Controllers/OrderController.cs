using Pizza_Ordering.Models;
using Pizza_Ordering.Models.Order;
using Pizza_Ordering.Models.OrderViewModel;
using Pizza_Ordering.Services.BLs;
using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pizza_Ordering.Controllers
{
    [RoutePrefix("api/order")]
    public class OrderController : BaseController
    {
        private IOrderBL _orderBL;

        public OrderController(IOrderBL orderBL)
        {
            _orderBL = orderBL;
        }

        [Route("GetCurrent")]
        [HttpGet]
        public IHttpActionResult GetCurrent()
        {
            DateTime now = DateTime.Now;
            int min = DateTime.Now.Minute;
            DateTime start = now - TimeSpan.FromMinutes((min / 10 * 10) + 10);
            return Json(_orderBL.GetOrderItemsSince(start).Select(o => new OrderViewModel(o)).ToList());
        }

        [Route("GetNew")]
        [HttpGet]
        public IHttpActionResult GetNew()
        {
            DateTime now = DateTime.Now;
            int min = DateTime.Now.Minute;
            DateTime start = now - TimeSpan.FromMinutes((min / 10 * 10) + 10);
            return Json(_orderBL.GetOrderItemsSince(start, false).Select(o => new OrderViewModel(o)).ToList());
        }

        [Route("accept/{id:long}")]
        [HttpPost]
        public void Accept(int id)
        {
            _orderBL.Accept(id);
        }

        [Route("reject/{id:long}")]
        [HttpPost]
        public void Reject(int id)
        {
            _orderBL.Reject(id);
        }

        [HttpPost]
        public void Post(CreateOrderModel model)
        {
            _orderBL.CreateOrder(new OrderDto
            {
                PizzaHouseId = model.PizzaHouseId,
                UserId = UserIsAuthenticated ? UserId : (long?)null,
                Name = model.Name,
                //Start = model.Start,
                //End = model.End,
                //TimeToTake = model.TimeToTake,
                Status = Common.PizzaStatusType.Processed,
                Items = model.Items.Select(x => new OrderItemDto
                {
                    PizzaId = x.PizzaId
                }).ToList()
            });
        }
    }
}