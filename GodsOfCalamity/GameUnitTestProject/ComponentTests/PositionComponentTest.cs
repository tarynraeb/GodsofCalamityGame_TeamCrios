
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GodsOfCalamity;

namespace GameUnitTestProject
{
    [TestClass]
    public class PositionComponentTest
    {
        [TestMethod]
        public void TestPositionComponent()
        {
            // Arrange
            int initX = 0;
            int initY = 0;
            int newX = 1;
            int newY = 1;
            GodsOfCalamity.Components.PositionComponent componentTest = new GodsOfCalamity.Components.PositionComponent(initX, initY);
            // Act
            componentTest.ChangePos(newX, newY);
            // Assert
            Assert.AreEqual(componentTest.getXPos(), newX);
            Assert.AreEqual(componentTest.getYPos(), newY);
        }
    }
}
