using System;
using System.Collections.Generic;

namespace ScooterRentalService
{
    public class RentalCalculator : IRentalCalculator
    {
        public decimal CalculateRent(DateTime endTime, DateTime startTime, decimal price)
        {
            decimal total = 0;

            var daysUsed = (endTime - startTime).TotalDays;

            if (daysUsed > 1)
            {
                var firstDayMins = CalculateMinutes(startTime);

                if (CalculateDaysRent(firstDayMins, price) > 20)
                {
                    total += 20;
                }
                else
                {
                    total += CalculateDaysRent(firstDayMins, price);
                }

                var lastDayMins = CalculateMinutes(endTime);

                if (CalculateDaysRent(lastDayMins, price) > 20)
                {
                    total += 20;
                }
                else
                {
                    total += CalculateDaysRent(lastDayMins, price);
                }

                if (daysUsed > 2)
                {
                    for (var i = 1; i <= daysUsed; i++)
                    {
                        var day = startTime.AddDays(1);
                        var minutes = CalculateMinutes(day);

                        if (CalculateDaysRent(minutes, price) > 20)
                        {
                            total += 20;
                        }
                        else
                        {
                            total += CalculateDaysRent(minutes, price);
                        }
                    }
                }
            }
            else
            {
                var totalMinutes = (endTime - startTime).TotalMinutes;
                total += CalculateDaysRent(totalMinutes, price) > 20 ? 20 : CalculateDaysRent(totalMinutes, price);
            }

            return total;
        }

        private decimal CalculateDaysRent(double aMins, decimal aPrice)
        {
            return Math.Round((decimal)aMins * aPrice);
        }

        private double CalculateMinutes(DateTime time)
        {
            var dayHours = time.TimeOfDay.TotalHours;
            var hourMinutes = dayHours * 60;
            var dayMins = time.TimeOfDay.TotalMinutes + hourMinutes;

            return dayMins;
        }

        public void SaveIncome(DateTime time, decimal payment, Dictionary<int, decimal> incomeDictionary)
        {
            var year = time.Year;
            incomeDictionary.Add(year, payment);
        }
    }
}