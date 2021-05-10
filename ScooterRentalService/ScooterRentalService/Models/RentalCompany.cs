using ScooterRentalService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRentalService.Models
{
    public class RentalCompany : IRentalCompany
    {
        private readonly IScooterService _scooter;
        private IRentedScooters _rented;
        private List<IRentedScooters> _rentedList;
        private Dictionary<int, decimal> _income;
        private IRentalCalculator _calculator;
        public string Name { get; }

        public RentalCompany(IScooterService scooterService)
        {
            _calculator = new RentalCalculator();
            _scooter = scooterService;
            _income = new Dictionary<int, decimal>();
            _rentedList = new List<IRentedScooters>();
            Name = "ScootGo";
        }

        public void StartRent(string id)
        {
            var startTime = DateTime.Now;
            _rented = new RentedScooters(_scooter.GetScooterById(id).Id, _scooter.GetScooterById(id).PricePerMinute,
                startTime, true);
            _rentedList.Add(_rented);
        }

        public decimal EndRent(string id)
        {
            var endTime = DateTime.Now;
            DateTime firstTime = DateTime.Now;
            decimal price = 0;

            foreach (var entry in _rentedList)
            {
                if (entry.Id == id)
                {
                    _scooter.AddScooter(entry.Id, entry.Price);
                    price = entry.Price;
                    firstTime = entry.RentStart;
                }
            }

            var result = _calculator.CalculateRent(endTime, firstTime, price);

            _calculator.SaveIncome(firstTime, result, _income);

            return result;
        }

        private decimal CalculateRentedIncome()
        {
            decimal rentedIncome = 0;

            foreach (var entry in _rentedList)
            {
                var price = entry.Price;
                var firstTime = entry.RentStart;

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
            if (year != null)
            {
                foreach (var entry in _income)
                {
                    if (entry.Key == year)
                    {
                        total += entry.Value;
                    }
                }
            }
            else
            {
                total = _income.Values.Sum();
            }


            return total + rentedIncome;
        }
    }
}
