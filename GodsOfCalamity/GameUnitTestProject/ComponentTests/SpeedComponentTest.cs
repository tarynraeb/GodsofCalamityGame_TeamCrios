
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GodsOfCalamity;

namespace GameUnitTestProject
{
    [TestClass]
    public class SpeedComponentTest
    {
        [TestMethod]
        public void TestSpeedComponent()
        {
            // Arrange
            int InitialSpeed = 90;
            int newSpeed = 91;
            GodsOfCalamity.Components.SpeedComponent componentTest = new GodsOfCalamity.Components.SpeedComponent(InitialSpeed);
            // Act
            componentTest.SetCurrentSpeed(newSpeed);
            // Assert
            Assert.AreEqual(componentTest.GetSpeed(), newSpeed);
        }
    }
}
