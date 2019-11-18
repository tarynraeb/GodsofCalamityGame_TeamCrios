
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GodsOfCalamity;

namespace GameUnitTestProject
{
    [TestClass]
    public class SpawnComponentTest
    {
        [TestMethod]
        public void TestSpawnComponent()
        {
            // Arrange
            bool spawnStatus = true;            
            GodsOfCalamity.Components.SpawnComponent componentTest = new GodsOfCalamity.Components.SpawnComponent();
            // Act
            componentTest.SetSpawnStatus(spawnStatus);
            // Assert
            Assert.AreEqual(componentTest.GetSpawnStatus(), spawnStatus);
        }
    }
}
