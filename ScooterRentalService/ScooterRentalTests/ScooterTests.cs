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
            RentalCompany c = new RentalCompany();

            c.AddScooter("3", 0.5m);

            var test = new List<Scooter> { new Scooter("3", 0.5m) };

            var x = c.GetScooters();

            Assert.AreEqual(test[0].Id, x[0].Id);
            Assert.AreEqual(test[0].PricePerMinute, x[0].PricePerMinute);
        }

        [TestMethod]
        public void RemoveScooter_InputId_RemoveById()
        {
            RentalCompany c = new RentalCompany();

            c.AddScooter("3", 0.5m);
            c.AddScooter("25", 0.6m);

            c.RemoveScooter("25");

            Assert.AreEqual(2, c.GetScooters().Count);
        }

        [TestMethod]
        public void GetScooterById_SearchById_GetTheCorrectScooter()
        {
            RentalCompany c = new RentalCompany();
            c.AddScooter("3", 0.5m);

            var test = c.GetScooterById("3");

            Assert.AreEqual(c.GetScooters()[0], test);
        }

        [TestMethod]
        public void StartRent_ScooterId_RemovesScooterFromList()
        {
            RentalCompany c = new RentalCompany();
            c.AddScooter("3", 0.5m);

            c.StartRent("3");

            Assert.IsFalse(c.GetScooters().Count == 0);
        }

        [TestMethod]
        public void EndRent_ScooterId_PutsScooterBackInList()
        {
            RentalCompany c = new RentalCompany();
            c.AddScooter("3", 0.5m);

            c.StartRent("3");
            c.EndRent("3");

            Assert.AreEqual("3", c.GetScooters()[0].Id);
        }

        [TestMethod]
        public void EndRent_30mins_OutputPrice()
        {
            RentalCompany c = new RentalCompany();
            c.AddScooter("3", 0.5m);

            c.StartRent("3");
            var result = c.EndRent("3");

            Assert.AreEqual(15, result);
        }


    }
}
