using System;
using System.Collections.Generic;

namespace ScooterRentalService
{
    class RentalCalculator : IRentalCalculator
    {
        public decimal CalculateRent(DateTime endTime, DateTime startTime, decimal price)
        {
            var minsUsed = (endTime - startTime).TotalMinutes;
            var result = Math.Round((decimal)minsUsed * price);

            return result;
        }

        public void SaveIncome(DateTime time, decimal payment, Dictionary<int, decimal> incomeDictionary)
        {
            var year = time.Year;
            incomeDictionary.Add(year, payment);
        }
    }
}