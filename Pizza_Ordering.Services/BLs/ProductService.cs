using Pizza_Ordering.DataProvider;
using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.Services
{
    public class ProductService : IProductService
    {
        public ProductDto GetById(long id)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var entity = ctx.SavedPizzas.FirstOrDefault(x => x.Id == id);

                if (entity == null)
                {
                    throw new ArgumentException(string.Format("entity with 'id' = {0} does not exist", id));
                }

                var res = new ProductDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Price = entity.BasePizza.Price // + add ingredients
                };

                return res;
            }
        }

        public void CreateProduct(ProductDto dto)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var entity = ctx.SavedPizzas.FirstOrDefault(x => x.Id == dto.Id);

                if (entity == null)
                {
                    throw new ArgumentException(string.Format("product with 'id' = {0} already exist", dto.Id));
                }

               /* entity = new Pizza_Ordering.Domain.
                {
                    Name = dto.Name,
                    Price = dto.Price
                };

                ctx.Products.Add(entity);*/
            }
        }

        public void UpdateProduct(ProductDto dto)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(long id)
        {
            throw new NotImplementedException();
        }
    }
}
