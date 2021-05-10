﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRentalService.Interfaces;
using ScooterRentalService.Models;
using ScooterRentalService.Service;
using System.Collections.Generic;

namespace ScooterRental.UnitTest
{
    [TestClass]
    public class ScooterServiceTests
    {
        private IScooterService _scooterService;
        private IRentalCompany _company;

        public ScooterServiceTests()
        {
            _scooterService = new ScooterService();
            _company = new RentalCompany(_scooterService);
        }

        [TestMethod]
        public void GetScooterById_ForLoop_ZeroIteration()
        {
            var result = _scooterService.GetScooterById("3");

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GetScooterById_ForLoop_OneIteration()
        {
            var test = new Scooter("3", 0.5m);
            _scooterService.AddScooter("3", 0.5m);

            var result = _scooterService.GetScooterById("3");

            Assert.AreEqual(test.Id, result.Id);
            Assert.AreEqual(test.PricePerMinute, result.PricePerMinute);
        }

        [TestMethod]
        public void AddScooter_AddOne_ExistsOne()
        {
            _scooterService.AddScooter("1", 0.2m);

            Assert.AreEqual(1, _scooterService.GetScooters().Count);
        }

        [TestMethod]
        public void RentRentedScooter()
        {
            _scooterService.AddScooter("1", 0.5m);
            _company.StartRent("1");
            _company.StartRent("1");
        }

        [TestMethod]
        public void GetScooters_ScooterUser_ListWithUser()
        {
            _scooterService.AddScooter("3", 0.5m);

            var test = new List<Scooter> { new Scooter("3", 0.5m) };

            var x = _scooterService.GetScooters();

            Assert.AreEqual(test[0].Id, x[0].Id);
            Assert.AreEqual(test[0].PricePerMinute, x[0].PricePerMinute);
        }

        [TestMethod]
        public void RemoveScooter_InputId_RemoveById()
        {
            _scooterService.AddScooter("3", 0.5m);
            _scooterService.AddScooter("25", 0.6m);

            _scooterService.RemoveScooter("25");

            Assert.AreEqual(1, _scooterService.GetScooters().Count);
        }

        [TestMethod]
        public void GetScooterById_SearchById_GetTheCorrectScooter()
        {
            _scooterService.AddScooter("3", 0.5m);

            var test = _scooterService.GetScooterById("3");

            Assert.AreEqual(_scooterService.GetScooters()[0], test);
        }
    }
}
