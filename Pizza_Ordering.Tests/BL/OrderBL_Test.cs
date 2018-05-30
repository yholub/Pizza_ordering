using System;
using Moq;
using Pizza_Ordering.DataProvider.Repositories;
using Pizza_Ordering.Domain.Entities;
using Pizza_Ordering.DataProvider.UnitOfWork;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using Pizza_Ordering.Services.BLs;
using Pizza_Ordering.Common;

namespace Pizza_Ordering.Tests.BL
{

    [TestFixture]
    public class OrderBL_Test
    {
        private Mock<IRepository<PizzaHouse>> housesRepo;
        private Mock<IRepository<Ingredient>> ingsRepo;
        private Mock<IRepository<IngredientAmount>> ingsAmRepo;
        private Mock<IRepository<FixPizza>> pizzasRepo;
        private Mock<IRepository<OrderItem>> orderItemsRepo;
        private Mock<IRepository<Pizza_Ordering.Domain.Entities.Order>> ordersRepo;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IUnitOfWorkFactory> unitOfWorkFactory;

        private List<PizzaHouse> houses;
        private List<Ingredient> ings;
        private List<IngredientAmount> ingAms;
        private List<FixPizza> fixPizzas;

        private List<OrderItem> orderItems;
        private List<Pizza_Ordering.Domain.Entities.Order> orders;
        private SettingEditDto currSettings;

        [SetUp]
        public void TestSetup()
        {
            ingsRepo = new Mock<IRepository<Ingredient>>();
            ingsAmRepo = new Mock<IRepository<IngredientAmount>>();
            housesRepo = new Mock<IRepository<PizzaHouse>>();
            pizzasRepo = new Mock<IRepository<FixPizza>>();
            orderItemsRepo = new Mock<IRepository<OrderItem>>();
            ordersRepo = new Mock<IRepository<Domain.Entities.Order>>();
            unitOfWork = new Mock<IUnitOfWork>();
            unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();

            currSettings = new SettingEditDto
            {
                StartHour = 9,
                EndHour = 23,
                Capacity = 3,
                PizzaHouseId = 0,
                IngState = new List<IngState>()
            };

            ings = new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Сіль", Price = 10, Weight = 10 },
                new Ingredient { Id = 2, Name = "Перець", Price = 10, Weight = 5 }
            };

            houses = new List<PizzaHouse>
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
                    ModeratorId = 0,
                    Capacity = 3,
                    InStock = new List<IngredientAmount>(),
                    Id = 0
                },

                new PizzaHouse
                {
                    Id = 1,
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
                    ModeratorId = 1,
                    Capacity = 3,
                    InStock = new List<IngredientAmount>()
                },
            };

            ingAms = new List<IngredientAmount>
            {
                new IngredientAmount
                {
                    Id = 0,
                    IngredientId = 1,
                    PizzaHouseId = 0,
                    House = houses[0],
                    Ingredient = ings[0]
                }
            };

            houses[0].InStock = new List<IngredientAmount> { ingAms[0] };

            fixPizzas = new List<FixPizza>
            {
                new FixPizza
                {
                    Name = "Маргарита",
                    Price = 50,
                    IngredientItems =
                        new List<IngredientItem>
                        {
                            new IngredientItem
                            {
                                Id = 10,
                                IngredientId = ings[0].Id,
                                Quantity = 1
                            }
                        }
                },
            };

            orderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    Id = 101,
                    PizzaId = fixPizzas[0].Id,
                    StartTime = DateTime.Today + TimeSpan.FromHours(5),
                    Price = 50,
                    EndTime = DateTime.Today + TimeSpan.FromHours(6),
                    OrderId = 20
                },

                new OrderItem
                {
                    Id = 102,
                    PizzaId = fixPizzas[0].Id,
                    StartTime = DateTime.Today + TimeSpan.FromHours(5),
                    Price = 50,
                    EndTime = DateTime.Today + TimeSpan.FromHours(6),
                    OrderId = 20
                },

                new OrderItem
                {
                    Id = 103,
                    PizzaId = fixPizzas[0].Id,
                    StartTime = DateTime.Today + TimeSpan.FromHours(6),
                    Price = 50,
                    EndTime = DateTime.Today + TimeSpan.FromHours(7),
                    OrderId = 50
                }
            };

            orders = new List<Domain.Entities.Order>
            {
                new Domain.Entities.Order
                {
                    Id = 20,
                    Price = 100,
                    PizzaHouse = houses[0],
                    PizzaHouseId = houses[0].Id,
                    Items = new List<OrderItem>
                    {
                        orderItems[0], orderItems[1]
                    },
                    TimeToTake = DateTime.Today + TimeSpan.FromHours(6)
                },
                new Domain.Entities.Order
                {
                    Id = 50,
                    Price = 50,
                    PizzaHouse = houses[0],
                    PizzaHouseId = houses[0].Id,
                    Items = new List<OrderItem>
                    {
                        orderItems[2]
                    },
                    TimeToTake = DateTime.Today + TimeSpan.FromHours(6)
                },
            };

            orderItems[0].Order = orders[0];
            orderItems[1].Order = orders[0];
            orderItems[2].Order = orders[1];

            unitOfWorkFactory.Setup(x => x.Create()).Returns(unitOfWork.Object);
            unitOfWork.Setup(x => x.PizzaHouses).Returns(housesRepo.Object);
            unitOfWork.Setup(x => x.Ingredients).Returns(ingsRepo.Object);
            unitOfWork.Setup(x => x.IngredientAmounts).Returns(ingsAmRepo.Object);
            unitOfWork.Setup(x => x.Orders).Returns(ordersRepo.Object);
            unitOfWork.Setup(x => x.FixPizzas).Returns(pizzasRepo.Object);
            unitOfWork.Setup(x => x.OrderItems).Returns(orderItemsRepo.Object);

            housesRepo.Setup(x => x.GetById(It.IsAny<long>())).Returns<long>(ind => houses.First(h => h.Id == ind));
            housesRepo.Setup(x => x.Query()).Returns(houses.AsQueryable());
            housesRepo.Setup(x => x.Update(It.IsAny<PizzaHouse>())).Callback<PizzaHouse>(h => Update(h));

            ingsRepo.Setup(x => x.Query()).Returns(ings.AsQueryable());
            ingsAmRepo.Setup(x => x.Query()).Returns(ingAms.AsQueryable());
            ingsAmRepo.Setup(x => x.Create(It.IsAny<IngredientAmount>())).Callback<IngredientAmount>(a => CreateIngAm(a));

            pizzasRepo.Setup(x => x.Query()).Returns(fixPizzas.AsQueryable());

            orderItemsRepo.Setup(x => x.Query()).Returns(orderItems.AsQueryable());

            ordersRepo.Setup(x => x.Query()).Returns(orders.AsQueryable());
            ordersRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns<int>(ind => orders.First(o => o.Id == ind));
        }

        [Test]
        public void CheckGetAllOrdersSince()
        {
            OrderBL bl = new OrderBL(unitOfWorkFactory.Object);
            var res = bl.GetOrderItemsSince(DateTime.Today + TimeSpan.FromHours(6), houses[0].Id);
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(orderItems[2].Id, res[0].Id);
        }

        [Test]
        public void CheckApprove()
        {
            OrderBL bl = new OrderBL(unitOfWorkFactory.Object);
            bl.Accept((int)orders[0].Id);
            Assert.AreEqual(orders[0].Status, PizzaStatusType.Accepted);
        }

        [Test]
        public void CheckDisapprove()
        {
            OrderBL bl = new OrderBL(unitOfWorkFactory.Object);
            bl.Reject((int)orders[0].Id);
            Assert.AreEqual(orders[0].Status, PizzaStatusType.Refused);
            //Assert.IsTrue(false);
        }

        private void Update(PizzaHouse h)
        {
            for (int i = 0; i < houses.Count; ++i)
            {
                if (h.Id == houses[i].Id)
                {
                    houses[i] = h;
                    return;
                }
            }
        }

        private void CreateIngAm(IngredientAmount am)
        {
            var house = houses.First(h => am.PizzaHouseId == h.Id);
            house.InStock.Add(am);
            ingAms.Add(am);
        }
    }
}
