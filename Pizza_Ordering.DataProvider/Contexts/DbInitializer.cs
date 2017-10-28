using Pizza_Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.DataProvider
{
    public class DbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            List<Ingredient> defaultIngredients = new List<Ingredient>
            {
                new Ingredient { Name = "моцарела", Price = 24, Weight = 100 },
                new Ingredient { Name = "пармезан", Price = 12, Weight = 10 },
                new Ingredient { Name = "фета", Price = 15, Weight = 50 },
                new Ingredient { Name = "балик", Price = 24, Weight = 100 },
                new Ingredient { Name = "шинка", Price = 25, Weight = 100 },
                new Ingredient { Name = "артишок", Price = 40, Weight = 60 },
                new Ingredient { Name = "салямі", Price = 14, Weight = 30 },
                new Ingredient { Name = "ковбаски мисливські", Price = 12, Weight = 30 },
                new Ingredient { Name = "банан", Price = 8, Weight = 50 },
                new Ingredient { Name = "перець болгарський", Price = 10, Weight = 40 },
                new Ingredient { Name = "курка", Price = 21, Weight = 100 },
                new Ingredient { Name = "бекон", Price = 23, Weight = 100 },
                new Ingredient { Name = "ананас", Price = 14, Weight = 40 },
                new Ingredient { Name = "баклажани", Price = 7, Weight = 40 },
                new Ingredient { Name = "перець пепероні", Price = 10, Weight = 10 },
                new Ingredient { Name = "помідори", Price = 10, Weight = 50 },
                new Ingredient { Name = "петрушка", Price = 5, Weight = 5 },
                new Ingredient { Name = "печериці", Price = 6, Weight = 50 },
                new Ingredient { Name = "маслини", Price = 22, Weight = 30 },
                new Ingredient { Name = "кукурудза", Price = 14, Weight = 50 },
                new Ingredient { Name = "айсберг", Price = 15, Weight = 50 },
                new Ingredient { Name = "броколі", Price = 15, Weight = 150 },
                new Ingredient { Name = "рукола", Price = 7, Weight = 15 },
                new Ingredient { Name = "базилік", Price = 6, Weight = 10 }
            };

            context.Ingredients.AddRange(defaultIngredients);

            // маргарита
            string[] pizzaIngr0Names = { "помідори", "базилік", "моцарела" };
            var pizzaIngr0 = defaultIngredients
                .Where(x => pizzaIngr0Names.Any(i => i == x.Name))
                .Select(x => new IngredientItem { Ingredient = x, Quantity = 1 })
                .ToList();

            // цезареі
            string[] pizzaIngr1Names = { "курка", "айсберг", "моцарела", "пармезан" };
            var pizzaIngr1 = defaultIngredients
                .Where(x => pizzaIngr1Names.Any(i => i == x.Name))
                .Select(x => new IngredientItem { Ingredient = x, Quantity = 1 })
                .ToList();

            // вегетеріанська
            string[] pizzaIngr2Names = { "рукола", "помідори", "базилік", "броколі", "баклажани", "перець болгарський", "моцарела" };
            var pizzaIngr2 = defaultIngredients
                .Where(x => pizzaIngr2Names.Any(i => i == x.Name))
                .Select(x => new IngredientItem { Ingredient = x, Quantity = 1 })
                .ToList();

            // капрічоза
            string[] pizzaIngr3Names = { "шинка", "артишоки", "печериці", "оливки", "моцарела" };
            var pizzaIngr3 = defaultIngredients
                .Where(x => pizzaIngr3Names.Any(i => i == x.Name))
                .Select(x => new IngredientItem { Ingredient = x, Quantity = 1 })
                .ToList();

            // гавайська
            string[] pizzaIngr4Names = { "курка", "ананас", "моцарела", "кукурудза" };
            var pizzaIngr4 = defaultIngredients
                .Where(x => pizzaIngr1Names.Any(i => i == x.Name))
                .Select(x => new IngredientItem { Ingredient = x, Quantity = 1 })
                .ToList();

            // українська
            string[] pizzaIngr5Names = { "шинка", "кукурудза", "печериці", "оливки", "помідори", "перець болгарський", "моцарела" };
            var pizzaIngr5 = defaultIngredients
                .Where(x => pizzaIngr2Names.Any(i => i == x.Name))
                .Select(x => new IngredientItem { Ingredient = x, Quantity = 1 })
                .ToList();

            List<FixPizza> fixPizzas = new List<FixPizza>
            {
                new FixPizza { Name = "Основа", Price = 20 },
                new FixPizza { Name = "Маргарита", Price = 55, IngredientItems = pizzaIngr0 },
                new FixPizza { Name = "Цезаре", Price = 120, IngredientItems = pizzaIngr1 },
                new FixPizza { Name = "Вегетеріанська", Price = 93, IngredientItems = pizzaIngr2 },
                new FixPizza { Name = "Капрічіоза", Price = 95, IngredientItems = pizzaIngr3 },
                new FixPizza { Name = "Гавайська", Price = 96, IngredientItems = pizzaIngr4 },
                new FixPizza { Name = "Українська", Price = 111, IngredientItems = pizzaIngr5 }
            };

            context.FixPizzas.AddRange(fixPizzas);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
