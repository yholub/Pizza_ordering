using Pizza_Ordering.DataProvider.UnitOfWork;
using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.BLs
{
    public class IngredientsBL : BaseBL
    {
        public IngredientsBL(IUnitOfWorkFactory factory)
            : base(factory)
        {
        }

        public IEnumerable<IngredientDto> GetAllIngredientDTOs()
        {
            return UseDb(uow =>
            {
                var dtos = uow.Ingredients.Query().Select(x => new IngredientDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Weight = x.Weight
                }).ToList();

                return dtos;
            });
        }
    }
}
