using System;
using System.Collections.Generic;

namespace ScooterRentalService
{
    public class RentalCompany : IRentalCompany
    {
        private ScooterService scooter;
        private Dictionary<Scooter, DateTime> rentedList;
        public string Name { get; }

        public RentalCompany()
        {
            scooter = new ScooterService();
            rentedList = new Dictionary<Scooter, DateTime>();
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            scooter.AddScooter(id, pricePerMinute);
        }

        public Scooter RemoveScooter(string id)
        {
            return scooter.RemoveScooter(id);
        }

        public IList<Scooter> GetScooters()
        {
            return scooter.GetScooters();
        }

        public Scooter GetScooterById(string scooterId)
        {
            return scooter.GetScooterById(scooterId);
        }

        public void StartRent(string id)
        {
            var startTime = DateTime.Now;
            rentedList.Add(scooter.RemoveScooter(id), startTime);

        }

        public decimal EndRent(string id)
        {
            var endTime = DateTime.Now.AddMinutes(30);
            DateTime firstTime = DateTime.Now;
            Decimal price = 0;

            foreach (KeyValuePair<Scooter, DateTime> entry in rentedList)
            {
                if (entry.Key.Id == id)
                {
                    scooter.AddScooter(entry.Key.Id, entry.Key.PricePerMinute);
                    price = entry.Key.PricePerMinute;
                    firstTime = entry.Value;
                }
            }

            var minsUsed = (endTime - firstTime).TotalMinutes;
            var result = (decimal)minsUsed * price;

            return Math.Round(result);
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            throw new NotImplementedException();
        }
    }
}
