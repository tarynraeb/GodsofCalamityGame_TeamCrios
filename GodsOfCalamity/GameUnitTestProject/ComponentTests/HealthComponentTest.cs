using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GodsOfCalamity;

namespace GameUnitTestProject.ComponentTests
{
    [TestClass]
    class HealthComponentTest
    {
        [TestMethod]
        public void TestHealthComponentConstructerAndGet()
        {
            // Arrange
            int testHealth = 9001;
            // Act
            GodsOfCalamity.Components.HealthComponent healthComponentTest = new GodsOfCalamity.Components.HealthComponent(testHealth);
            // Assert
            Assert.AreEqual(healthComponentTest.getHealth(), testHealth);
        }

        [TestMethod]
        public void TestSetHealth()
        {
            // Arrange
            int testHealth = 9001;
            GodsOfCalamity.Components.HealthComponent healthComponentTest = new GodsOfCalamity.Components.HealthComponent(testHealth);

            // Act
            healthComponentTest.setHealth(9000);

            // Assert
            Assert.AreEqual(healthComponentTest.getHealth(), 9000);
        }
    }
}
