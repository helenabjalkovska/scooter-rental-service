using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRentalService;
using System.Collections.Generic;

namespace ScooterRentalTests
{
    [TestClass]
    public class ScooterTests
    {
        [TestMethod]
        public void GetScooters_ScooterUser_ListWithUser()
        {
            ScooterService c = new ScooterService();

            c.AddScooter("3", 0.5m);

            var test = new List<Scooter> { new Scooter("3", 0.5m) };

            var x = c.GetScooters();

            Assert.AreEqual(test[0].Id, x[0].Id);
            Assert.AreEqual(test[0].PricePerMinute, x[0].PricePerMinute);
        }

        [TestMethod]
        public void RemoveScooter_InputId_RemoveById()
        {
            ScooterService c = new ScooterService();

            c.AddScooter("3", 0.5m);
            c.AddScooter("25", 0.6m);

            c.RemoveScooter("25");

            Assert.AreEqual(1, c.GetScooters().Count);
        }

        [TestMethod]
        public void GetScooterById_SearchById_GetTheCorrectScooter()
        {
            ScooterService c = new ScooterService();
            c.AddScooter("3", 0.5m);

            var test = c.GetScooterById("3");

            Assert.AreEqual(c.GetScooters()[0], test);
        }
    }
}
