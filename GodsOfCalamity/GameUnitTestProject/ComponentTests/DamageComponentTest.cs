
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GodsOfCalamity;

namespace GameUnitTestProject
{
    [TestClass]
    public class DamageComponentTest
    {
        [TestMethod]
        public void TestDamageComponent()
        {
            // Arrange
            int initialDamage = 90;
            int newDamage = 91;
            GodsOfCalamity.Components.DamageComponent componentTest = new GodsOfCalamity.Components.DamageComponent(initialDamage);
            // Act
            componentTest.setInflictableDamage(newDamage);
            // Assert
            Assert.AreEqual(componentTest.getInflictableDamage(), newDamage);
        }
    }
}
