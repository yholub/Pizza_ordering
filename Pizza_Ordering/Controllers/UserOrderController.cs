using Pizza_Ordering.Common;
using Pizza_Ordering.Models.Order;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Pizza_Ordering.Controllers
{
    [RoutePrefix("api/order")]
    public class UserOrderController : BaseController
    {
        private readonly IOrderBL _orderBL;

        public UserOrderController(IOrderBL orderBL)
        {
            _orderBL = orderBL;
        }

        [HttpPost]
        public IHttpActionResult Post(CreateOrderModel model)
        {
            _orderBL.CreateOrder(new Services.DTOs.OrderDto
            {
                Name = "Замовлення",
                UserId = UserId,
                PizzaHouseId = 1,
                // Price should be calculated
                Price = 0,
                Status = PizzaStatusType.Processed,
                TimeToTake = model.Items.Max(x => x.EndTime)
            });
            
            return Ok();
        }
    }
}