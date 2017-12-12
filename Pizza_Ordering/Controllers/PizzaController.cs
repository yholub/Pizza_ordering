using Pizza_Ordering.Models.Order;
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

        [HttpPost]
        public List<PizzaHouseDto> Get(OrderBindingModel model)
        {
            int qty = model.OrderItems.Sum(o => o.Count);
            var houses = _service.GetPizzaHouses();

            var checkIngs = model.OrderItems

                .SelectMany(o => o.Ingredients
                    .Select( i => new { Ing = i, Am = o.Count }) )
                .GroupBy(i => i.Ing.Id)
                .Select(g => new
                {
                    Id = g.Key,
                    Count = g.Sum(i => i.Ing.Count * i.Am)
                })
                .Where(g => g.Count > 0);

            var filteredHouses = houses.Where(h =>
            {
                return checkIngs.All(i =>
                {
                    var found = h.InStock.FirstOrDefault(el => el.IngredientDto.Id == i.Id);
                    if (found == null)
                        return false;

                    return found.Quantity >= i.Count;
                });

            });

            return filteredHouses.ToList();
        }
    }
}