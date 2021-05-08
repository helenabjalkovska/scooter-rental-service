using ScooterRentalService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRentalService.Models
{
    public class RentalCompany : IRentalCompany
    {
        private readonly IScooterService _scooter;
        private Dictionary<Scooter, DateTime> _rentedList;
        private Dictionary<int, decimal> _income;
        private IRentalCalculator _calculator;
        public string Name { get; }

        public RentalCompany(IScooterService scooterService)
        {
            _calculator = new RentalCalculator();
            _scooter = scooterService;
            _rentedList = new Dictionary<Scooter, DateTime>();
            _income = new Dictionary<int, decimal>();
            Name = "ScootGo";
        }

        public void StartRent(string id)
        {
            var startTime = DateTime.Now;
            //_rentedList.Add(_scooter.RemoveScooter(id), startTime);

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

            var result = _calculator.CalculateRent(endTime, firstTime, price);

            _calculator.SaveIncome(firstTime, result, _income);

            return result;
        }

        private decimal CalculateRentedIncome()
        {
            decimal rentedIncome = 0;

            foreach (KeyValuePair<Scooter, DateTime> entry in _rentedList)
            {
                var price = entry.Key.PricePerMinute;
                var firstTime = entry.Value;

                rentedIncome += _calculator.CalculateRent(DateTime.Now, firstTime, price);
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
