using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.Interfaces
{
    public interface IPizzaHouseBL
    {
        List<PizzaHouseDto> GetPizzaHouses();

        PizzaHouseDto GetPizzaHouse(long moderatorId);

        void UpdatePizzaHouse(SettingEditDto dto);
    }
}
