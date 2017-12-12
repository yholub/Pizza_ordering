using Moq;
using NUnit.Framework;
using Pizza_Ordering.DataProvider.Repositories;
using Pizza_Ordering.DataProvider.UnitOfWork;
using Pizza_Ordering.Domain.Entities;
using Pizza_Ordering.Services.BLs;
using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using Pizza_Ordering.Tests.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Tests.BL
{
    [TestFixture]
    public class PizzaHouseBL_Test
    {
        private Mock<IRepository<PizzaHouse>> housesRepo;
        private Mock<IRepository<Ingredient>> ingsRepo;
        private Mock<IRepository<IngredientAmount>> ingsAmRepo;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IUnitOfWorkFactory> unitOfWorkFactory;
        private Mock<IPizzaHouseBL> ingredientsBL;

        private List<PizzaHouse> houses;
        private List<Ingredient> ings;
        private List<IngredientAmount> ingAms;
        private SettingEditDto currSettings;

        [SetUp]
        public void TestSetup()
        {
            ingsRepo = new Mock<IRepository<Ingredient>>();
            ingsAmRepo = new Mock<IRepository<IngredientAmount>>();
            housesRepo = new Mock<IRepository<PizzaHouse>>();
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

            unitOfWorkFactory.Setup(x => x.Create()).Returns(unitOfWork.Object);
            unitOfWork.Setup(x => x.PizzaHouses).Returns(housesRepo.Object);
            unitOfWork.Setup(x => x.Ingredients).Returns(ingsRepo.Object);
            unitOfWork.Setup(x => x.IngredientAmounts).Returns(ingsAmRepo.Object);

            housesRepo.Setup(x => x.GetById(It.IsAny<long>())).Returns<long>(ind => houses.First(h => h.Id == ind));
            housesRepo.Setup(x => x.Query()).Returns(houses.AsQueryable());
            housesRepo.Setup(x => x.Update(It.IsAny<PizzaHouse>())).Callback<PizzaHouse>(h => Update(h));

            ingsRepo.Setup(x => x.Query()).Returns(ings.AsQueryable());
            ingsAmRepo.Setup(x => x.Query()).Returns(ingAms.AsQueryable());
            ingsAmRepo.Setup(x => x.Create(It.IsAny<IngredientAmount>())).Callback<IngredientAmount>(a => CreateIngAm(a));
        }

        [Test]
        public void ShouldGetAllHouses()
        {
            //Arrange
            var target = new PizzaHouseBL(unitOfWorkFactory.Object);

            //Act
            var actual = target.GetPizzaHouses();

            //Assert
            Assert.AreEqual(houses.Count, actual.Count());
            CollectionAssert.AreEqual(houses, actual, new PizzaHouseEntityDtoComparer());
        }

        [Test]
        public void ShouldGetCorrectHouse()
        {
            //Arrange
            var target = new PizzaHouseBL(unitOfWorkFactory.Object);

            //Act
            var actual = target.GetPizzaHouse(0);
            var comparer = new PizzaHouseEntityDtoComparer();
            var res = comparer.Compare(houses[0], actual);
            //Assert
            Assert.AreEqual(0, res, "Incorrect house returned");
        }

        [Test]
        public void ShouldUpdateStartHourSettings()
        {
            //Arrange
            var target = new PizzaHouseBL(unitOfWorkFactory.Object);
            currSettings.StartHour = 10;
            target.UpdatePizzaHouse(currSettings);

            //Assert
            Assert.AreEqual(10, houses[0].OpenTime.Hours, "Incorrectly time is updated");
        }

        [Test]
        public void ShouldUpdateCloseHourSettings()
        {
            //Arrange
            var target = new PizzaHouseBL(unitOfWorkFactory.Object);
            currSettings.EndHour = 10;
            target.UpdatePizzaHouse(currSettings);

            //Assert
            Assert.AreEqual(10, houses[0].CloseTime.Hours, "Incorrectly time is updated");
        }

        [Test]
        public void ShouldUpdateCapacitySettings()
        {
            //Arrange
            var target = new PizzaHouseBL(unitOfWorkFactory.Object);
            currSettings.Capacity = 10;
            target.UpdatePizzaHouse(currSettings);

            //Assert
            Assert.AreEqual(10, houses[0].Capacity);
        }

        [Test]
        public void ShouldUpdateIngredientAmSettings()
        {
            //Arrange
            var target = new PizzaHouseBL(unitOfWorkFactory.Object);
            currSettings.IngState = new List<IngState> {
                new IngState {
                    Id = 1,
                    Quantity = 2
                },
                new IngState {
                    Id = 2,
                    Quantity = 4
                }
            };

            target.UpdatePizzaHouse(currSettings);

            //Assert
            Assert.AreEqual(2, houses[0].InStock[0].Quantity);
            Assert.AreEqual(4, houses[0].InStock[1].Quantity);
        }

        [Test]
        public void GetCorrectHouseById()
        {
            //Arrange
            var target = new PizzaHouseBL(unitOfWorkFactory.Object);
            var res = target.GetPizzaHouseById(houses[0].Id);

            //Assert
            Assert.AreEqual(res.Id, houses[0].Id);
            Assert.AreEqual(res.ModeratorId, houses[0].ModeratorId);
        }

        private void Update(PizzaHouse h)
        {
            for (int i = 0; i < houses.Count; ++i)
            {
                if(h.Id == houses[i].Id)
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
