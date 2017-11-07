using Pizza_Ordering.DataProvider.Contexts;
using Pizza_Ordering.Domain.Entities;
using Pizza_Ordering.Domain.Identity;
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
                new Ingredient { Id = 1, Name = "моцарела", Price = 24, Weight = 100 },
                new Ingredient { Id = 2, Name = "пармезан", Price = 12, Weight = 10 },
                new Ingredient { Id = 3, Name = "балик", Price = 24, Weight = 100 },
                new Ingredient { Id = 4, Name = "шинка", Price = 25, Weight = 100 },
                new Ingredient { Id = 5, Name = "артишок", Price = 40, Weight = 60 },
                new Ingredient { Id = 6, Name = "салямі", Price = 14, Weight = 30 },
                new Ingredient { Id = 7, Name = "ковбаски мисливські", Price = 12, Weight = 30 },
                new Ingredient { Id = 8, Name = "перець болгарський", Price = 10, Weight = 40 },
                new Ingredient { Id = 9, Name = "курка", Price = 21, Weight = 100 },
                new Ingredient { Id = 10, Name = "бекон", Price = 23, Weight = 100 },
                new Ingredient { Id = 11, Name = "ананас", Price = 14, Weight = 40 },
                new Ingredient { Id = 12, Name = "баклажани", Price = 7, Weight = 40 },
                new Ingredient { Id = 13, Name = "помідори", Price = 10, Weight = 50 },
                new Ingredient { Id = 14, Name = "петрушка", Price = 5, Weight = 5 },
                new Ingredient { Id = 15, Name = "печериці", Price = 6, Weight = 50 },
                new Ingredient { Id = 16, Name = "маслини", Price = 22, Weight = 30 },
                new Ingredient { Id = 17, Name = "кукурудза", Price = 14, Weight = 50 },
                new Ingredient { Id = 18, Name = "айсберг", Price = 15, Weight = 50 },
                new Ingredient { Id = 19, Name = "броколі", Price = 15, Weight = 150 },
                new Ingredient { Id = 20, Name = "рукола", Price = 7, Weight = 15 },
                new Ingredient { Id = 21, Name = "базилік", Price = 6, Weight = 10 }
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
                new FixPizza { Name = "Капрічоза", Price = 95, IngredientItems = pizzaIngr3 },
                new FixPizza { Name = "Гавайська", Price = 96, IngredientItems = pizzaIngr4 },
                new FixPizza { Name = "Українська", Price = 111, IngredientItems = pizzaIngr5 }
            };

            context.FixPizzas.AddRange(fixPizzas);
            context.SaveChanges();

            var authContext = new AuthorizationContext();
            var umanager = new ApplicationUserManager(new CustomUserStore(authContext));
            var roleManager = new ApplicationRoleManager(new CustomRoleStore(authContext));
            var task = roleManager.CreateAsync(new CustomRole
            {
                Name = "Moderator"
            });

            task.Wait();
            User u = new User {
                Name = "Andriy",
                Email = "mod1@g",
                UserName = "mod1@g"
            };

            var taskU = umanager.CreateAsync(u, "123456") ;
            taskU.Wait();

            User u2 = new User
            {
                Name = "Andriy2",
                Email = "mod2@g",
                UserName = "mod2@g"
            };

            var taskU2 = umanager.CreateAsync(u2, "123456");
            taskU2.Wait();

            var taskR = umanager.AddToRoleAsync(u.Id, "Moderator");
            taskR.Wait();

            var taskR2 = umanager.AddToRoleAsync(u2.Id, "Moderator");
            taskR2.Wait();

            List<PizzaHouse> pizzas = new List<PizzaHouse>
            {
                new PizzaHouse
                {
                    OpenTime = TimeSpan.FromHours(9),
                    CloseTime = TimeSpan.FromHours(23),
                    Location = new Address
                    {
                        City = "Львів",
                        Street = "Левицького",
                        District = "13",
                        HouseNumber = "2",
                        Lat = 49.8360502,
                        Lng = 24.0352977
                    },
                    ModeratorId = u.Id,
                    Capacity = 3
                },

                new PizzaHouse
                {
                    OpenTime = TimeSpan.FromHours(9),
                    CloseTime = TimeSpan.FromHours(23),
                    Location = new Address
                    {
                        City = "Львів",
                        Street = "Січових Стрільців",
                        District = "13",
                        HouseNumber = "7",
                        Lat = 49.840367,
                        Lng = 24.024088
                    },
                    ModeratorId = u2.Id,
                    Capacity = 3
                },
            };

            context.PizzaHouses.AddRange(pizzas);

            context.SaveChanges();

            List<Order> orders = new List<Order>
            {
                new Order
                {
                    PizzaHouseId = pizzas[0].Id,
                    Price = 80,
                    TimeToTake = FromTime(22, 40),
                    Status = Pizza_Ordering.Common.PizzaStatusType.Processed
                },

                new Order
                {
                    PizzaHouseId = pizzas[0].Id,
                    Price = 180,
                    TimeToTake = FromTime(23, 20),
                    Status = Pizza_Ordering.Common.PizzaStatusType.Processed
                },

                new Order
                {
                    PizzaHouseId = pizzas[0].Id,
                    Price = 90,
                    TimeToTake = FromTime(23, 50),
                    Status = Common.PizzaStatusType.Processed
                },
            };

            context.Orders.AddRange(orders);

            context.SaveChanges();

            List<OrderItem> items = new List<OrderItem>
            {
                new OrderItem
                {
                    OrderId = orders[0].Id,
                    PizzaId = fixPizzas[1].Id,
                    Price = 80,
                    StartTime = FromTime(22, 20),
                    EndTime = FromTime(22, 40)
                },
                new OrderItem
                {
                    OrderId = orders[1].Id,
                    PizzaId = fixPizzas[3].Id,
                    Price = 100,
                    StartTime = FromTime(23, 00),
                    EndTime = FromTime(23, 20)
                },
                new OrderItem
                {
                    OrderId = orders[1].Id,
                    PizzaId = fixPizzas[1].Id,
                    Price = 80,
                    StartTime = FromTime(23, 00),
                    EndTime = FromTime(23, 20)
                },
                new OrderItem
                {
                    OrderId = orders[2].Id,
                    PizzaId = fixPizzas[2].Id,
                    Price = 90,
                    StartTime = FromTime(23, 10),
                    EndTime = FromTime(23, 50)
                }
            };

            context.OrderItems.AddRange(items);
            context.SaveChanges();

            base.Seed(context);
        }

        private static DateTime FromTime(int h, int m)
        {
            return DateTime.Today + TimeSpan.FromMinutes(m) + TimeSpan.FromHours(h);
        }
    }
}
