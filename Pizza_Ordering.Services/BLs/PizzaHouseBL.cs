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
    public class PizzaHouseBL : BaseBL, IPizzaHouseBL
    {
        public PizzaHouseBL(IUnitOfWorkFactory factory)
            : base(factory)
        {
        }

        public List<DTOs.PizzaHouseDto> GetPizzaHouses()
        {
            return UseDb(db =>
            {
                return db.PizzaHouses
                    .Query()
                    .Select(p => new PizzaHouseDto
                    {
                        Id = p.Id,
                        Location = new AddressDto
                        {
                            Lon = p.Location.Lng,
                            Lat = p.Location.Lat,
                            City = p.Location.City,
                            HouseNumber = p.Location.HouseNumber,
                            StreetName = p.Location.Street,
                        },
                        Close = p.CloseTime.Hours,
                        Open = p.OpenTime.Hours,
                        ModeratorId = p.ModeratorId,
                        Capacity = p.Capacity,
                        InStock = p.InStock.Select(s => new IngredientQuantityDto { 
                            Quantity = s.Quantity,
                            Id = s.Id,
                            IngredientDto = new IngredientDto
                            {
                                Id = s.Ingredient.Id,
                                Name = s.Ingredient.Name,
                                Price = s.Ingredient.Price
                            }
                        }).ToList()
                    }).ToList();
            });
        }

        public DTOs.PizzaHouseDto GetPizzaHouse(long moderatorId)
        {
            return UseDb(db =>
            {
                return db.PizzaHouses
                    .Query()
                    .Where( h => h.ModeratorId == moderatorId)
                    .Select(p => new PizzaHouseDto
                    {
                        
                        Id = p.Id,
                        Location = new AddressDto
                        {
                            Lon = p.Location.Lng,
                            Lat = p.Location.Lat,
                            City = p.Location.City,
                            HouseNumber = p.Location.HouseNumber,
                            StreetName = p.Location.Street,
                        },
                        Close = p.CloseTime.Hours,
                        Open = p.OpenTime.Hours,
                        ModeratorId = p.ModeratorId,
                        Capacity = p.Capacity,
                        InStock = p.InStock.Select(s => new IngredientQuantityDto
                        {
                            Quantity = s.Quantity,
                            Id = s.Id,
                            IngredientDto = new IngredientDto
                            {
                                Id = s.Ingredient.Id,
                                Name = s.Ingredient.Name
                            }
                        }).ToList()
                    }).First();
            });
        }

        public void UpdatePizzaHouse(SettingEditDto dto)
        {
            UseDb(db =>
            {
                PizzaHouse entity = db.PizzaHouses.GetById(dto.PizzaHouseId);
                entity.Capacity = dto.Capacity;
                dto.StartHour = dto.StartHour % 24;
                dto.EndHour = dto.EndHour % 24;
                entity.OpenTime = TimeSpan.FromHours(dto.StartHour);
                entity.CloseTime = TimeSpan.FromHours(dto.EndHour);
                db.PizzaHouses.Update(entity);

                var ams = db.IngredientAmounts.Query()
                    .Where(l => l.PizzaHouseId == dto.PizzaHouseId);

                foreach(var am in dto.IngState) 
                {
                    var e = ams.FirstOrDefault(a => a.IngredientId == am.Id);
                    if (e == null)
                    {
                        e = new IngredientAmount
                        {
                            IngredientId = am.Id,
                            PizzaHouseId = dto.PizzaHouseId,
                            Quantity = am.Quantity
                        };

                        db.IngredientAmounts.Create(e);
                    }
                    else
                    {
                        e.Quantity = am.Quantity;
                        db.IngredientAmounts.Update(e);
                    }
                }

                db.Save();
            });
        }
    }
}
