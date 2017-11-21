using Pizza_Ordering.Common;
using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.Interfaces
{
    public interface IPizzasBL
    {
        List<PizzaDto> GetFixPizzas();

        List<PizzaDto> GetSavedPizzas(long userId);

        PizzaDto GetPizzaById(PizzaType pizzaType, long pizzaId);

        long CreateFixPizza(PizzaDto pizzaDto);

        long CreateSavedPizza(long userId, PizzaDto pizzaDto);

        long CreateModifiedPizza(PizzaDto pizzaDto);

        void DeleteFixPizza(long pizzaId);
    }
}
