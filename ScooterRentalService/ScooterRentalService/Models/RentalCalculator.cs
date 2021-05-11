using System;
using System.Collections.Generic;

namespace ScooterRentalService
{
    class RentalCalculator : IRentalCalculator
    {
        public decimal CalculateRent(DateTime endTime, DateTime startTime, decimal price)
        {
            decimal total = 0;
            var minsUsed = (endTime - startTime).TotalMinutes;
            var result = Math.Round((decimal)minsUsed * price);

            if (result > 20)
            {
                result = 0;
                total += 20;
                TimeSpan ts = new TimeSpan(0, 0, 0);
                startTime = DateTime.Today.AddDays(1) + ts;
            }
            else
            {
                total += result;
            }

            return total;
        }

        public void SaveIncome(DateTime time, decimal payment, Dictionary<int, decimal> incomeDictionary)
        {
            var year = time.Year;
            incomeDictionary.Add(year, payment);
        }
    }
}