using Moq;
using NUnit.Framework;
using Pizza_Ordering.DataProvider.Repositories;
using Pizza_Ordering.DataProvider.UnitOfWork;
using Pizza_Ordering.Domain.Entities;
using Pizza_Ordering.Services.BLs;
using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Tests.BL
{
    [TestFixture]
    public class IngredientsBL_Test
    {
        private Mock<IRepository<Ingredient>> ingredientRepo;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IUnitOfWorkFactory> unitOfWorkFactory;
        private Mock<IIngredientsBL> ingredientsBL;

        private List<Ingredient> data;

        [SetUp]
        public void TestSetup()
        {
            ingredientRepo = new Mock<IRepository<Ingredient>>();
            unitOfWork = new Mock<IUnitOfWork>();
            unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            ingredientsBL = new Mock<IIngredientsBL>();

            data = new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Сіль", Price = 10, Weight = 10 }
            };

            unitOfWorkFactory.Setup(x => x.Create()).Returns(unitOfWork.Object);
            unitOfWork.Setup(x => x.Ingredients).Returns(ingredientRepo.Object);
            ingredientRepo.Setup(x => x.Query()).Returns(data.AsQueryable());
        }

        [Test]
        public void ShouldGetAllIngredients()
        {
            //Arrange
            var target = new IngredientsBL(unitOfWorkFactory.Object);
            var expected = data.Select(x => new IngredientDto
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Weight = x.Weight
            }).ToList().AsEnumerable();

            //Act
            var actual = target.GetAll();

            //Assert
            Assert.AreEqual(data.Count, actual.Count());
            Assert.IsTrue(actual.SequenceEqual(expected));
        }
    }
}
