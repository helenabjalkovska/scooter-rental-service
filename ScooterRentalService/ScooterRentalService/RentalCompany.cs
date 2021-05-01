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
            Name = "ScootGo";
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
            var endTime = DateTime.Now;
            DateTime firstTime = DateTime.Now;
            decimal price = 0;

            foreach (KeyValuePair<Scooter, DateTime> entry in _rentedList)
            {
                if (entry.Key.Id == id)
                {
                    _scooter.AddScooter(entry.Key.Id, entry.Key.PricePerMinute);
                    price = entry.Key.PricePerMinute;
                    firstTime = entry.Value;
                }
            }

            var result = CalculateRent(endTime, firstTime, price);

            SaveIncome(firstTime, result);

            return result;
        }

        public decimal CalculateRent(DateTime endTime, DateTime startTime, decimal price)
        {
            var minsUsed = (endTime - startTime).TotalMinutes;

            return Math.Round((decimal)minsUsed * price);
        }

        public void SaveIncome(DateTime time, decimal payment)
        {
            var year = time.Year;
            _income.Add(year, payment);
        }

        public decimal CalculateRentedIncome()
        {
            decimal rentedIncome = 0;

            foreach (KeyValuePair<Scooter, DateTime> entry in _rentedList)
            {
                var price = entry.Key.PricePerMinute;
                var firstTime = entry.Value;

                rentedIncome += CalculateRent(DateTime.Now, firstTime, price);
            }

            return rentedIncome;
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            decimal rentedIncome = 0;
            if (includeNotCompletedRentals)
            {
                if (_rentedList.Count > 0)
                {
                    rentedIncome = CalculateRentedIncome();
                }
            }

            decimal total = 0;
            foreach (var entry in _income)
            {
                if (entry.Key == year)
                {
                    total += entry.Value;
                }
            }

            return total + rentedIncome;
        }

        public decimal CalculateIncome(bool includeNotCompletedRentals)
        {
            decimal rentedIncome = 0;
            if (includeNotCompletedRentals)
            {
                if (_rentedList.Count > 0)
                {
                    rentedIncome = CalculateRentedIncome();
                }
            }

            var total = _income.Values.Sum() + rentedIncome;
            return total;
        }
    }
}
