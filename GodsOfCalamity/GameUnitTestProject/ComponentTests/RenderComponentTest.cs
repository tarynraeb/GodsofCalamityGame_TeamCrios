
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GodsOfCalamity;

namespace GameUnitTestProject
{
    [TestClass]
    public class RenderComponentTest
    {
        [TestMethod]
        public void TestRenderComponentConstructorAndGet()
        {
            // Arrange
            string testPath = "this is a path";
            // Act
            GodsOfCalamity.Components.RenderComponent componentTest = new GodsOfCalamity.Components.RenderComponent(testPath);            
            // Assert
            Assert.AreEqual(componentTest.GetAssetRef(), testPath);            // asserts are used to figure out if the test actually passed
        }

        [TestMethod]
        public void TestRenderComponentNewPath()
        {
            // Arrange
            string testPath = "this is a path";
            string newPath = "this is also a path";
            GodsOfCalamity.Components.RenderComponent componentTest = new GodsOfCalamity.Components.RenderComponent(testPath);
            // Act
            componentTest.SetAssetPath(newPath);
            // Assert
            Assert.AreEqual(componentTest.GetAssetRef(), newPath);
        }
    }
}
