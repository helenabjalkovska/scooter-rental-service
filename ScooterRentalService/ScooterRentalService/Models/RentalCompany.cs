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
            if (_scooter.GetScooterById(id) == null)
            {
                throw new ScooterNotFoundException();
            }
            var startTime = DateTime.Now;
            var scooter = _scooter.GetScooterById(id);
            _rented = new RentedScooters(scooter.Id, scooter.PricePerMinute, startTime, true);
            _rentedList.Add(_rented);
        }

        public decimal EndRent(string id)
        {
            if (_scooter.GetScooterById(id) == null)
            {
                throw new ScooterNotFoundException();
            }

            var endTime = DateTime.Now.AddMinutes(40);
            DateTime firstTime = DateTime.Now;
            decimal price = 0;

            foreach (var entry in _rentedList)
            {
                if (entry.Id == id)
                {
                    entry.IsRented = false;
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
                rentedIncome = CalculateRentedIncome();
            }

            decimal total = 0;
            if (year != null)
            {
                if (!_income.Values.ToList().Contains(Convert.ToInt32(year)))
                {
                    throw new IncomeNotFoundException();
                }

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
