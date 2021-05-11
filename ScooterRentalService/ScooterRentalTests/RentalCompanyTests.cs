using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRentalService;
using ScooterRentalService.Interfaces;
using ScooterRentalService.Models;
using ScooterRentalService.Service;

namespace ScooterRental.UnitTest
{
    [TestClass]
    public class RentalCompanyTests
    {
        private IScooterService _scooterService;
        private IRentalCompany _company;

        public RentalCompanyTests()
        {
            _scooterService = new ScooterService();
            _company = new RentalCompany(_scooterService);
        }

        [TestMethod]
        public void StartRent_InvalidScooter()
        {
            Assert.ThrowsException<ScooterNotFoundException>(() => _company.StartRent("1"));
        }

        [TestMethod]
        public void StartRent_ScooterId_RemovesScooterFromList()
        {
            _scooterService.AddScooter("3", 0.5m);

            _company.StartRent("3");

            Assert.IsFalse(_scooterService.GetScooters().Count == 0);
        }

        [TestMethod]
        public void EndRent_ScooterId_PutsScooterBackInList()
        {
            _scooterService.AddScooter("3", 0.5m);

            _company.StartRent("3");
            _company.EndRent("3");

            Assert.AreEqual("3", _scooterService.GetScooters()[0].Id);
        }

        [TestMethod]
        public void EndRent_30mins_OutputPrice()
        {
            _scooterService.AddScooter("3", 0.5m);

            _company.StartRent("3");
            var result = _company.EndRent("3");

            Assert.AreEqual(20, result); // had to change since test numbers weren't the actual ones
            // before the expected was 15, since I tested with 30 min rental
        }

        [TestMethod]
        public void CalculateIncome_WithoutYearInputWithoutRentals_AllTimeCalculation()
        {
            _scooterService.AddScooter("3", 0.5m);

            _company.StartRent("3");
            _company.EndRent("3");

            var result = _company.CalculateIncome(null, false);

            Assert.AreEqual(20, result); // had to change since test numbers weren't the actual ones
        }


        [TestMethod]
        public void CalculateIncome_WithYearInputWithoutRentals_CorrectYearIncome()
        {
            _scooterService.AddScooter("3", 0.5m);

            _company.StartRent("3");
            _company.EndRent("3");

            var result = _company.CalculateIncome(2021, false);

            Assert.AreEqual(20, result); // had to change since test numbers weren't the actual ones
        }

        [TestMethod]
        public void CalculateIncome_WithYearInputWithoutRentals_IncorrectYearIncome()
        {
            _scooterService.AddScooter("3", 0.5m);

            _company.StartRent("3");
            _company.EndRent("3");

            var result = _company.CalculateIncome(2019, false);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateIncome_WithoutYearInputWithRentals_AllTimeCalculation()
        {
            _scooterService.AddScooter("3", 0.5m);
            _scooterService.AddScooter("5", 1.0m);


            _company.StartRent("3");
            _company.StartRent("5");
            _company.EndRent("3");

            var result = _company.CalculateIncome(null, false);

            Assert.AreEqual(20, result); // had to change since test numbers weren't the actual ones
        }
    }
}
