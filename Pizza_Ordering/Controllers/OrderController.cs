using Pizza_Ordering.Models;
using Pizza_Ordering.Services.BLs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pizza_Ordering.Controllers
{
   
    public class OrderController : BaseController
    {
        private IOrderBL _service;
        public OrderController(IOrderBL service)
        {
            _service = service;
        }

        [Route("api/order/GetCurrent")]
        [HttpGet]
        public IHttpActionResult GetCurrent()
        {
            DateTime now = DateTime.Now;
            int min = DateTime.Now.Minute;
            DateTime start = now - TimeSpan.FromMinutes(min / 10 * 10 + 10);
            return Json(_service.GetOrdersSince(start).Select(o => new OrderViewModel(o)).ToList());
        }

        [Route("api/order/GetNew")]
        [HttpGet]
        public IHttpActionResult GetNew()
        {
            DateTime now = DateTime.Now;
            int min = DateTime.Now.Minute;
            DateTime start = now - TimeSpan.FromMinutes(min / 10 * 10 + 10);
            return Json(_service.GetPendingOrdersSince(start).Select(o => new OrderViewModel(o)).ToList());
        }

        [Route("{id:long}")]
        [HttpPost]
        public void Accept(int id)
        {

            _service.Accept(id);
        }

        [Route("{id:long}")]
        [HttpPost]
        public void Reject(int id)
        {
            _service.Reject(id);
        }

    }
}
