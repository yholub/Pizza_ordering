using Pizza_Ordering.DataProvider.Repositories;
using Pizza_Ordering.Domain.Entities;
using Pizza_Ordering.Models.Ingredient;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Pizza_Ordering.Controllers
{
    [RoutePrefix("api/ingredients")]
    public class IngredientsController : BaseController
    {
        private readonly IIngredientsBL _ingredientsBL;

        public IngredientsController(IIngredientsBL ingredientsBL)
        {
            _ingredientsBL = ingredientsBL;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var dtos = _ingredientsBL.GetAll();
            var model = dtos.Select(dto => new IngredientModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
                Weight = dto.Weight
            }).ToList();

            return Json(model);
        }

        [Route("{id:long}")]
        [HttpGet]
        public IHttpActionResult Get(long id)
        {
            var dto = _ingredientsBL.GetById(id);
            var model = new IngredientModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
                Weight = dto.Weight
            };

            return Json(model);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]CreateIngredientModel model)
        {
            _ingredientsBL.Create(new Services.DTOs.IngredientDto
            {
                Name = model.Name,
                Price = model.Price,
                Weight = model.Weight
            });

            return Ok();
        }

        [Route("{id:long}")]
        [HttpPut]
        public IHttpActionResult Put(long id, [FromBody]UpdateIngredientModel model)
        {
            // not working yet
            _ingredientsBL.Update(new Services.DTOs.IngredientDto
            {
                Id = id,
                Name = model.Name,
                Price = model.Price,
                Weight = model.Weight
            });

            return Ok();
        }

        [Route("{id:long}")]
        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            _ingredientsBL.Delete(id);

            return Ok();
        }
    }
}