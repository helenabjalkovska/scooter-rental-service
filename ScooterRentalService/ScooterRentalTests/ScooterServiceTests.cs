using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRentalService.Interfaces;
using ScooterRentalService.Models;
using ScooterRentalService.Service;
using System.Collections.Generic;

namespace ScooterRental.UnitTest
{
    class ScooterServiceTests
    {
        [TestMethod]
        public void AddScooter_AddOne_ExistsOne()
        {

        }

        [TestMethod]
        public void RentRentedScooter()
        {
            IScooterService service = new ScooterService();
            IRentalCompany company = new RentalCompany(service);

            service.AddScooter("1", 0.5m);
            company.StartRent("1");
            company.StartRent("1");
        }

        [TestMethod]
        public void GetScooters_ScooterUser_ListWithUser()
        {
            IScooterService service = new ScooterService();
            IRentalCompany company = new RentalCompany(service);

            service.AddScooter("3", 0.5m);

            var test = new List<Scooter> { new Scooter("3", 0.5m) };

            var x = service.GetScooters();

            Assert.AreEqual(test[0].Id, x[0].Id);
            Assert.AreEqual(test[0].PricePerMinute, x[0].PricePerMinute);
        }

        [TestMethod]
        public void RemoveScooter_InputId_RemoveById()
        {
            IScooterService service = new ScooterService();
            IRentalCompany company = new RentalCompany(service);

            service.AddScooter("3", 0.5m);
            service.AddScooter("25", 0.6m);

            service.RemoveScooter("25");

            Assert.AreEqual(2, service.GetScooters().Count);
        }

        [TestMethod]
        public void GetScooterById_SearchById_GetTheCorrectScooter()
        {
            IScooterService service = new ScooterService();
            IRentalCompany company = new RentalCompany(service);
            service.AddScooter("3", 0.5m);

            var test = service.GetScooterById("3");

            Assert.AreEqual(service.GetScooters()[0], test);
        }


    }
}
