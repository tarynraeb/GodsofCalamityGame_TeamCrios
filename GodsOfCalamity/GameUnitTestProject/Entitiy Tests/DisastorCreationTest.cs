using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GodsOfCalamity;
using GodsOfCalamity.Entity;

namespace GameUnitTestProject.Entitiy_Tests
{ 
    [TestClass]
    class EntityManagerTests
    {
        [TestMethod]
        public void TestCreateDisasterEntity()
        {
            EntityType testtype = EntityType.Fire;
            EntityManager testmanager = new EntityManager();
            BaseEntity entitytest = testmanager.CreateDisasterEntity("Fire");
            Assert.AreEqual(entitytest.GetType(), testtype);

            testtype = EntityType.Lightning;
            entitytest = testmanager.CreateDisasterEntity("Lightning");
            Assert.AreEqual(entitytest.GetType(), testtype);

            testtype = EntityType.Meteor;
            entitytest = testmanager.CreateDisasterEntity("Meteor");
            Assert.AreEqual(entitytest.GetType(), testtype);

        }
        [TestMethod]
        public void TestOtherCreationTypes()
        {
            EntityManager testmanager = new EntityManager();
            EntityType testtype = EntityType.Background;
            BaseEntity testentity = testmanager.GiveBackground();
            Assert.AreEqual(testentity.GetType(), testtype);

            testtype = EntityType.Village;
            testentity = testmanager.GiveVillage();
            Assert.AreEqual(testentity.GetType(), testtype);

            testtype = EntityType.HealthBar;
            testentity = testmanager.GiveHealthBar();
            Assert.AreEqual(testentity.GetType(), testtype);

            testtype = EntityType.PauseButton;
            testentity = testmanager.GivePauseButton();
            Assert.AreEqual(testentity.GetType(), testtype);

            testtype = EntityType.MenuResume;
            testentity = testmanager.GiveMenuResume();
            Assert.AreEqual(testmanager.GetType(), testtype);

            testtype = EntityType.MenuQuit;
            testentity = testmanager.GiveMenuQuit();
            Assert.AreEqual(testentity.GetType(), testtype);

            testtype = EntityType.PlayerCursor;
            testentity = testmanager.GiveCursor();
            Assert.AreEqual(testentity.GetType(), testtype);

        }

    }
}
