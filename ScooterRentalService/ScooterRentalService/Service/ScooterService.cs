using ScooterRentalService.Interfaces;
using ScooterRentalService.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRentalService.Service
{
    public class ScooterService : IScooterService
    {
        private List<Scooter> _scooters;

        public ScooterService()
        {
            _scooters = new List<Scooter>();
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            if (pricePerMinute == 0)
            {
                throw new ZeroPriceException();
            }
            _scooters.Add(new Scooter(id, pricePerMinute));
        }

        public void RemoveScooter(string id)
        {
            var found = false;
            for (var i = 0; i < _scooters.Count; i++)
            {
                if (_scooters[i].Id == id)
                {
                    found = true;
                    _scooters.RemoveAt(i);
                }
            }

            if (!found)
            {
                throw new ScooterNotFoundException();
            }
        }

        public IList<Scooter> GetScooters()
        {
            if (_scooters.ToList().Count == 0)
            {
                throw new ScooterNotFoundException("Scooters not found");
            }
            return _scooters.ToList();
        }

        public Scooter GetScooterById(string scooterId)
        {
            for (var i = 0; i < _scooters.Count; i++)
            {
                if (_scooters[i].Id == scooterId)
                {
                    return _scooters[i];
                }
            }

            throw new ScooterNotFoundException();
        }
    }
}
