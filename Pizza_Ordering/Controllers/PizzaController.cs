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
    public class PizzaController : ApiController
    {
        private IPizzaHouseBL _service;

        public PizzaController(IPizzaHouseBL service)
        {
            _service = service;
        }
        // GET api/<controller>
        [HttpGet]
        public List<PizzaHouseDto> Get()
        {
            return _service.GetPizzaHouses();
        }
    }
}