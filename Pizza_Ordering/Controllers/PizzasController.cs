using Pizza_Ordering.Common;
using Pizza_Ordering.Models;
using Pizza_Ordering.Models.Pizza;
using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Pizza_Ordering.Controllers
{
    [RoutePrefix("api/pizzas")]
    public class PizzasController : BaseController
    {
        private readonly IPizzasBL _pizzasBL;

        public PizzasController(IPizzasBL pizzasBL)
        {
            _pizzasBL = pizzasBL;
        }

        [Route("fix")]
        [HttpGet]
        public IHttpActionResult GetFixPizzas()
        {
            var dtos = _pizzasBL.GetFixPizzas();
            var models = dtos;

            return Json(models);
        }

        [Route("saved/{userId:long}")]
        [HttpGet]
        public IHttpActionResult GetSavedPizzas(long userId)
        {
            var dtos = _pizzasBL.GetSavedPizzas(userId);
            var models = dtos;

            return Json(models);
        }

        [Route("{pizzaType}/{id:long}")]
        [HttpGet]
        public IHttpActionResult Get(PizzaType pizzaType, long id)
        {
            var dto = _pizzasBL.GetPizzaById(pizzaType, id);
            var model = dto;

            return Json(model);
        }

        [Route("fix")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]CreateFixPizzaModel model)
        {
            PizzaDto dto = new PizzaDto
            {
                PizzaType = PizzaType.Fix,
                Name = model.Name,
                Price = model.Price,
                Ingredients = model.Ingredients
            };

            _pizzasBL.CreateFixPizza(dto);

            return Ok();
        }

        [Route("saved")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]CreateSavedPizzaModel model)
        {
            PizzaDto dto = new PizzaDto
            {
                BasePizzaId = model.BasePizzaId,
                PizzaType = PizzaType.Saved,
                Name = model.Name,
                Price = model.Price,
                Ingredients = model.Ingredients
            };

            return Ok();
        }

        [Route("fix/{id:long}")]
        [HttpDelete]
        public IHttpActionResult DeleteFixPizza(long id)
        {
            _pizzasBL.DeleteFixPizza(id);

            return Ok();
        }
    }
}