using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphVisualization;
using GraphVisualization.Controllers;

namespace GraphVisualization.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShortestPath()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.ShortestPath(-1, -1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
