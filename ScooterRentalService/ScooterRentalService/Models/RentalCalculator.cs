using System;
using System.Collections.Generic;

namespace ScooterRentalService
{
    public class RentalCalculator : IRentalCalculator
    {
        public decimal CalculateRent(DateTime endTime, DateTime startTime, decimal price)
        {
            decimal total = 0;

            var daysUsed = (endTime.Day - startTime.Day);

            if (daysUsed > 0)
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
            var endDay = new DateTime(time.Year, time.Month, time.AddDays(1).Day, 00, 00, 00);
            var dayHours = 24 + (endDay.TimeOfDay - time.TimeOfDay).Hours;
            var fullMin = dayHours == 24 ? 0 : 60;
            var hourMinutes = dayHours > 1 ? dayHours * fullMin : 0;
            var dayMins = fullMin == 0 ? time.TimeOfDay.Minutes + hourMinutes : fullMin - time.TimeOfDay.Minutes + hourMinutes;

            return dayMins;
        }

        public void SaveIncome(DateTime time, decimal payment, Dictionary<int, decimal> incomeDictionary)
        {
            var year = time.Year;
            incomeDictionary.Add(year, payment);
        }
    }
}