using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace Pizza_Ordering.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class SettingsController : ApiController
    {
        private IPizzaHouseBL _service;

        public SettingsController(IPizzaHouseBL service)
        {
            _service = service;
        }

        [HttpGet]
        public PizzaHouseDto Get()
        {
            return _service.GetPizzaHouse(User.Identity.GetUserId<long>());
        }


        [HttpPost]
        public void Post(SettingEditDto set)
        {
            var h = _service.GetPizzaHouse(User.Identity.GetUserId<long>());
            set.PizzaHouseId = h.Id;
            _service.UpdatePizzaHouse(set);
        }
    }
}
