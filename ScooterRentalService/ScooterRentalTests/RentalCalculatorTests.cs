﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            DateTime startTime = new DateTime(2021, 05, 10, 08, 00, 00);
            decimal price = 0.5m;

            var result = _calculator.CalculateRent(endtime, startTime, price);

            Assert.AreEqual(40, result);
        }
    }
}
