using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRentalService;
using System;

namespace ScooterRental.Tests
{
    [TestClass]
    public class RentalCalculatorTests
    {
        private RentalCalculator _calculator;
        public RentalCalculatorTests()
        {
            _calculator = new RentalCalculator();
        }

        [TestMethod]
        public void CalculateRent_30minutes()
        {
            DateTime endtime = DateTime.Now.AddMinutes(30);
            DateTime startTime = DateTime.Now;
            decimal price = 0.5m;

            var result = _calculator.CalculateRent(endtime, startTime, price);

            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void CalculateRent_50Minutes()
        {
            DateTime endtime = DateTime.Now.AddMinutes(50);
            DateTime startTime = DateTime.Now;
            decimal price = 0.5m;

            var result = _calculator.CalculateRent(endtime, startTime, price);

            Assert.AreEqual(20, result);
        }

        [TestMethod]
        public void CalculateRent_2FullDays()
        {
            DateTime endtime = DateTime.Now;
            DateTime yesterday = endtime.AddDays(-1);
            DateTime startTime = new DateTime(2021, yesterday.Month, yesterday.Day, 08, 00, 00);
            decimal price = 0.5m;

            var result = _calculator.CalculateRent(endtime, startTime, price);

            Assert.AreEqual(40, result);
        }

        [TestMethod]
        public void CalculateRent_4FullDays()
        {
            DateTime endtime = DateTime.Now;
            DateTime yesterday = endtime.AddDays(-3);
            DateTime startTime = new DateTime(2021, yesterday.Month, yesterday.Day, 08, 00, 00);
            decimal price = 0.5m;

            var result = _calculator.CalculateRent(endtime, startTime, price);

            Assert.AreEqual(80, result);
        }

        [TestMethod]
        public void CalculateRent_2Days_FirstEveningShort()
        {
            DateTime endtime = DateTime.Now;
            DateTime yesterday = endtime.AddDays(-1);
            DateTime startTime = new DateTime(2021, yesterday.Month, yesterday.Day, 23, 50, 00);
            decimal price = 0.5m;

            var result = _calculator.CalculateRent(endtime, startTime, price);

            Assert.AreEqual(25, result);
        }

        [TestMethod]
        public void CalculateRent_2Days_LastMorningShort()
        {
            DateTime today = DateTime.Now;
            DateTime yesterday = DateTime.Now.AddDays(-1);
            DateTime endtime = new DateTime(2021, today.Month, today.Day, 00, 10, 00);
            DateTime startTime = new DateTime(2021, yesterday.Month, yesterday.Day, 23, 50, 00);
            decimal price = 0.5m;

            var result = _calculator.CalculateRent(endtime, startTime, price);

            Assert.AreEqual(10, result);
        }
    }
}
