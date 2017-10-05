using Pizza_Ordering.Models.Product;
using Pizza_Ordering.Services.Interfaces;
using Pizza_Ordering.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Pizza_Ordering.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController()
            : this(new ProductService()) { }

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        
        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var dto = _productService.GetById(id);
            var model = new ProductModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price
            };

            return Ok(model);
        }
        
        [HttpPost]
        public IHttpActionResult Post([FromBody]CreateProductModel model)
        {
            _productService.CreateProduct(new Services.DTOs.ProductDto
            {
                Name = model.Name,
                Price = model.Price
            });

            return Ok();
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]UpdateProductModel model)
        {
            _productService.UpdateProduct(new Services.DTOs.ProductDto
            {
                Id = id,
                Name = model.Name,
                Price = model.Price
            });

            return Ok();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);

            return Ok();
        }
    }
}