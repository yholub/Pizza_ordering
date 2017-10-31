using Pizza_Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.DTOs
{
    public class SettingsDto
    {
        public int StartHour { get; set; }

        public int EndHour { get; set; }

        public int Capacity { get; set; }

        public List<IngredientDto> Locked { get; set; }

        public SettingsDto()
        {
            StartHour = 9;
            EndHour = 24;
            Capacity = 2;
            Locked = new List<IngredientDto>
            {
                new IngredientDto { Name = "Моцарела", Price = 24, Weight = 100 },
                new IngredientDto { Name = "Пармезан", Price = 12, Weight = 10 },
                new IngredientDto { Name = "Фета", Price = 15, Weight = 50 },
                new IngredientDto { Name = "Балик", Price = 24, Weight = 100 },
                new IngredientDto { Name = "Артишок", Price = 40, Weight = 60 },
                new IngredientDto { Name = "Салямі", Price = 14, Weight = 30 },
                new IngredientDto { Name = "Ковбаски Мисливські", Price = 12, Weight = 30 },
                new IngredientDto { Name = "Банан", Price = 8, Weight = 50 },
                new IngredientDto { Name = "Перець Болгарський", Price = 10, Weight = 40 },
                new IngredientDto { Name = "Курка", Price = 21, Weight = 100 },
                new IngredientDto { Name = "Бекон", Price = 23, Weight = 100 },
                new IngredientDto { Name = "Ананас", Price = 14, Weight = 40 },
                new IngredientDto { Name = "Баклажани", Price = 7, Weight = 40 },
                new IngredientDto { Name = "Перець Пепероні", Price = 10, Weight = 10 },
                new IngredientDto { Name = "Помідори", Price = 10, Weight = 50 },
                new IngredientDto { Name = "Петрушка", Price = 5, Weight = 5 },
                new IngredientDto { Name = "Печериці", Price = 6, Weight = 50 },
                new IngredientDto { Name = "Маслини", Price = 22, Weight = 30 },
                new IngredientDto { Name = "Кукурудза", Price = 14, Weight = 50 }
            };

            for (int i = 0; i < Locked.Count; ++i)
            {
                Locked[i].Id = i;
            }
        }
    }
}
