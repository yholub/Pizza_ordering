using Pizza_Ordering.Domain.Entities;
using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Tests.Comparers
{
    class PizzaHouseEntityDtoComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            PizzaHouse xHouse = x as PizzaHouse;
            PizzaHouseDto yDto = y as PizzaHouseDto;
            if (xHouse == null || yDto == null)
                return -1;

            if (xHouse.Id == yDto.Id && xHouse.ModeratorId == yDto.ModeratorId
                && xHouse.OpenTime.Hours == yDto.Open && xHouse.CloseTime.Hours == yDto.Close
                && xHouse.Location.Street == yDto.Location.StreetName
                && xHouse.Location.HouseNumber == yDto.Location.HouseNumber
                && xHouse.Location.Lat == yDto.Location.Lat
                && xHouse.Location.Lng == yDto.Location.Lon
                && xHouse.Location.City == yDto.Location.City)
                return 0;

            return 1;

        }
    }
}
