using Pizza_Ordering.DataProvider.UnitOfWork;
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
                        Location = new AddressDto {
                            Lon = p.Location.Lng,
                            Lat = p.Location.Lat,
                            City = p.Location.City,
                            HouseNumber = p.Location.HouseNumber,
                            StreetName = p.Location.Street
                        },
                        Close = p.СloseTime,
                        Open = p.OpenTime
                        
                    }).ToList();
            });
        }
    }
}
