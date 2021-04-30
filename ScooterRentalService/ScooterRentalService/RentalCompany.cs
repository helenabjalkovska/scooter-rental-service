using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRentalService
{
    public class RentalCompany : IRentalCompany
    {
        private ScooterService _scooter;
        private Dictionary<Scooter, DateTime> _rentedList;
        private Dictionary<int, decimal> _income;
        public string Name { get; }

        public RentalCompany()
        {
            _scooter = new ScooterService();
            _rentedList = new Dictionary<Scooter, DateTime>();
            _income = new Dictionary<int, decimal>();
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            _scooter.AddScooter(id, pricePerMinute);
        }

        public Scooter RemoveScooter(string id)
        {
            return _scooter.RemoveScooter(id);
        }

        public IList<Scooter> GetScooters()
        {
            return _scooter.GetScooters();
        }

        public Scooter GetScooterById(string scooterId)
        {
            return _scooter.GetScooterById(scooterId);
        }

        public void StartRent(string id)
        {
            var startTime = DateTime.Now;
            _rentedList.Add(_scooter.RemoveScooter(id), startTime);

        }

        public decimal EndRent(string id)
        {
            var endTime = DateTime.Now.AddMinutes(30);
            DateTime firstTime = DateTime.Now;
            Decimal price = 0;

            foreach (KeyValuePair<Scooter, DateTime> entry in _rentedList)
            {
                if (entry.Key.Id == id)
                {
                    _scooter.AddScooter(entry.Key.Id, entry.Key.PricePerMinute);
                    price = entry.Key.PricePerMinute;
                    firstTime = entry.Value;
                }
            }

            var minsUsed = (endTime - firstTime).TotalMinutes;
            var result = Math.Round((decimal)minsUsed * price);

            SaveIncome(firstTime, result);

            return result;
        }

        public void SaveIncome(DateTime time, decimal payment)
        {
            var year = time.Year;
            _income.Add(year, payment);
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateIncome(bool includeNotCompletedRentals)
        {
            return _income.Values.Sum();
        }
    }
}
