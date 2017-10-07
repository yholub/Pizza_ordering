using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.Interfaces
{
    public interface IProductService
    {
        ProductDto GetById(long id);

        void CreateProduct(ProductDto dto);

        void UpdateProduct(ProductDto dto);

        void DeleteProduct(long id);
    }
}
