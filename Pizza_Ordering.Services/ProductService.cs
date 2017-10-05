using Pizza_Ordering.DataProvider;
using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services
{
    public class ProductService : IProductService
    {
        public ProductDto GetById(int id)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var entity = ctx.Products.FirstOrDefault(x => x.Id == id);

                if (entity == null)
                {
                    throw new ArgumentException($"entity with 'id' = {id} does not exist");
                }

                var res = new ProductDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Price = entity.Price
                };

                return res;
            }
        }

        public void CreateProduct(ProductDto dto)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var entity = ctx.Products.FirstOrDefault(x => x.Id == dto.Id);

                if (entity == null)
                {
                    throw new ArgumentException($"product with 'id' = {dto.Id} already exist");
                }

                entity = new Pizza_Ordering.Domain.Product
                {
                    Name = dto.Name,
                    Price = dto.Price
                };

                ctx.Products.Add(entity);
            }
        }

        public void UpdateProduct(int id, ProductDto dto)
        {
            throw new NotImplementedException();
        }
        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
