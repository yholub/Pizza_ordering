using Pizza_Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.DataProvider
{
    public class DbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            using (context)
            {
                List<Ingredient> defaultIngredients = new List<Ingredient>
                {
                    new Ingredient { Name = "Моцарела", Price = 24, Weight = 100 },
                    new Ingredient { Name = "Пармезан", Price = 12, Weight = 10 },
                    new Ingredient { Name = "Фета", Price = 15, Weight = 50 },
                    new Ingredient { Name = "Балик", Price = 24, Weight = 100 },
                    new Ingredient { Name = "Артишок", Price = 40, Weight = 60 },
                    new Ingredient { Name = "Салямі", Price = 14, Weight = 30 },
                    new Ingredient { Name = "Ковбаски Мисливські", Price = 12, Weight = 30 },
                    new Ingredient { Name = "Банан", Price = 8, Weight = 50 },
                    new Ingredient { Name = "Перець Болгарський", Price = 10, Weight = 40 },
                    new Ingredient { Name = "Курка", Price = 21, Weight = 100 },
                    new Ingredient { Name = "Бекон", Price = 23, Weight = 100 },
                    new Ingredient { Name = "Ананас", Price = 14, Weight = 40 },
                    new Ingredient { Name = "Баклажани", Price = 7, Weight = 40 },
                    new Ingredient { Name = "Перець Пепероні", Price = 10, Weight = 10 },
                    new Ingredient { Name = "Помідори", Price = 10, Weight = 50 },
                    new Ingredient { Name = "Петрушка", Price = 5, Weight = 5 },
                    new Ingredient { Name = "Печериці", Price = 6, Weight = 50 },
                    new Ingredient { Name = "Маслини", Price = 22, Weight = 30 },
                    new Ingredient { Name = "Кукурудза", Price = 14, Weight = 50 }
                };

                context.Ingredients.AddRange(defaultIngredients);

                string[] pizzaIngr1Names = { "Курка", "Ананас", "Моцарела" };
                var pizzaIngr1 = defaultIngredients.Where(x => pizzaIngr1Names.Any(i => i == x.Name)).ToList();
                string[] pizzaIngr2Names = { "Артишок", "Моцарела", "Балик" };
                var pizzaIngr2 = defaultIngredients.Where(x => pizzaIngr2Names.Any(i => i == x.Name)).ToList();
                string[] pizzaIngr3Names = { "Баклажани", "Перець Болгарський", "Моцарела" };
                var pizzaIngr3 = defaultIngredients.Where(x => pizzaIngr3Names.Any(i => i == x.Name)).ToList();
                List<FixPizza> fixPizzas = new List<FixPizza>
                {
                    new FixPizza { Name = "Гавайська", Price = 96, Ingredients = pizzaIngr1 },
                    new FixPizza { Name = "Капрічіоза", Price = 111, Ingredients = pizzaIngr2 },
                    new FixPizza { Name = "Вегетеріанська", Price = 83, Ingredients = pizzaIngr3 },
                };

                context.FixPizzas.AddRange(fixPizzas);

                context.SaveChanges();

                base.Seed(context);
            }
        }
    }
}
