using Moq;
using NUnit.Framework;
using Pizza_Ordering.DataProvider.Repositories;
using Pizza_Ordering.DataProvider.UnitOfWork;
using Pizza_Ordering.Domain.Entities;
using Pizza_Ordering.Services.BLs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Tests.BL
{
    [TestFixture]
    public class PizzaBL_Test
    {
        private Mock<IRepository<FixPizza>> fixPizzaRepo;
        private Mock<IRepository<ModifiedPizza>> modifiedPizzaRepo;
        private Mock<IRepository<SavedPizza>> savedPizzaRepo;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IUnitOfWorkFactory> unitOfWorkFactory;

        private List<FixPizza> fixPizzas;
        private List<SavedPizza> savedPizzas;

        [SetUp]
        public void TestSetup()
        {
            fixPizzaRepo = new Mock<IRepository<FixPizza>>();
            modifiedPizzaRepo = new Mock<IRepository<ModifiedPizza>>();
            savedPizzaRepo = new Mock<IRepository<SavedPizza>>();
            unitOfWork = new Mock<IUnitOfWork>();
            unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();

            fixPizzas = new List<FixPizza>
            {
                new FixPizza
                {
                    Id = 1,
                    Name = "PizzaName1",
                    Price = 200,
                    IngredientItems = new List<IngredientItem>
                    {
                        new IngredientItem
                        {
                            Id = 1,
                            Quantity = 2,
                            IngredientId = 1,
                            Ingredient = new Ingredient
                            {
                                Id = 1,
                                Name = "IngredientName1",
                                Price = 10,
                                Weight = 10
                            }
                        }
                    }
                }
            };

            savedPizzas = new List<SavedPizza>
            {
                new SavedPizza
                {
                    BasePizza = new FixPizza
                    {
                        Id = 1,
                        Name = "PizzaName1",
                        Price = 200,
                        IngredientItems = new List<IngredientItem>
                        {
                            new IngredientItem
                            {
                                Id = 1,
                                Quantity = 2,
                                IngredientId = 1,
                                Ingredient = new Ingredient
                                {
                                    Id = 1,
                                    Name = "IngredientName1",
                                    Price = 10,
                                    Weight = 10
                                }
                            }
                        }
                    },
                    FixPizzaId = 1,
                    Id = 1,
                    Name = "SavedPizza1",
                    UserId = 1,
                    User = new User
                    {
                        Name = "userName1",
                        UserName = "userUserName1",
                        Photo = "photo"
                    },
                    IngredientItems = new List<IngredientItem>
                    {
                        new IngredientItem
                        {
                            Id = 1,
                            Quantity = 1,
                            Ingredient = new Ingredient
                            {
                                Id = 1,
                                Name = "ingrName1",
                                Price = 10,
                                Weight = 10
                            },
                            IngredientId = 1
                        }
                    }
                }
            };

            unitOfWorkFactory.Setup(x => x.Create()).Returns(unitOfWork.Object);
            unitOfWork.Setup(x => x.FixPizzas).Returns(fixPizzaRepo.Object);
            unitOfWork.Setup(x => x.SavedPizzas).Returns(savedPizzaRepo.Object);
            unitOfWork.Setup(x => x.ModifiedPizzas).Returns(modifiedPizzaRepo.Object);
            fixPizzaRepo.Setup(x => x.Query()).Returns(fixPizzas.AsQueryable());
            savedPizzaRepo.Setup(x => x.Query()).Returns(savedPizzas.AsQueryable());
        }

        [Test]
        public void ShouldGetAllFixPizzas()
        {
            //Arrange
            var target = new PizzasBL(unitOfWorkFactory.Object);

            //Act
            var actual = target.GetFixPizzas();

            //Assert
            Assert.AreEqual(fixPizzas.Count, actual.Count());
            Assert.IsTrue(false);
        }

        [Test]
        public void ShouldGetAllSavedPizzas()
        {
            //Arrange
            var target = new PizzasBL(unitOfWorkFactory.Object);
            long userId1 = 1;
            long userId2 = 2;

            //Act
            var actual1 = target.GetSavedPizzas(userId1);
            var actual2 = target.GetSavedPizzas(userId2);

            //Assert
            Assert.AreEqual(savedPizzas.Count, actual1.Count());
            Assert.AreNotEqual(savedPizzas.Count, actual2.Count());
        }
    }
}
