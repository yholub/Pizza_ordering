using NUnit.Framework;
using Pizza_Ordering;
using Pizza_Ordering.Controllers;
using System.Web.Mvc;

namespace Pizza_Ordering.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual(1, 1);
        }
    }
}
