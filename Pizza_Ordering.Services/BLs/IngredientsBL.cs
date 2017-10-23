using Pizza_Ordering.DataProvider.UnitOfWork;
using Pizza_Ordering.Domain.Entities;
using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.BLs
{
    public class IngredientsBL : BaseBL, IIngredientsBL
    {
        public IngredientsBL(IUnitOfWorkFactory factory)
            : base(factory)
        {
        }

        public IEnumerable<IngredientDto> GetAll()
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

        public IngredientDto GetById(long id)
        {
            return UseDb(uow =>
            {
                var entity = uow.Ingredients.GetById(id);
                var dto = new IngredientDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Price = entity.Price,
                    Weight = entity.Weight
                };

                return dto;
            });
        }

        public void Create(IngredientDto dto)
        {
            UseDb(uow =>
            {
                var entity = new Ingredient
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Weight = dto.Weight
                };

                uow.Ingredients.Create(entity);

                uow.Save();
            });
        }

        public void Delete(long id)
        {
            UseDb(uow =>
            {
                uow.Ingredients.Delete(id);

                uow.Save();
            });
        }

        public void Update(IngredientDto dto)
        {
            UseDb(uow =>
            {
                var entity = uow.Ingredients.GetById(dto.Id);
                entity.Name = !string.IsNullOrEmpty(dto.Name) ? dto.Name : entity.Name;
                entity.Price = dto.Price != 0 ? dto.Price : entity.Price;
                entity.Weight = dto.Weight != 0 ? dto.Weight : entity.Weight;

                uow.Save();
            });
        }
    }
}
