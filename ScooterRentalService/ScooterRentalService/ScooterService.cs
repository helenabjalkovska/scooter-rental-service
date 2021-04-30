using System.Collections.Generic;

namespace ScooterRentalService
{
    public class ScooterService : IScooterService
    {
        private Scooter _test;
        private List<Scooter> _scooters;
        private List<Scooter> _toRemove;

        public ScooterService()
        {
            _scooters = new List<Scooter>();
            _toRemove = new List<Scooter>();
            _test = new Scooter("test", 1.1m);

            _scooters.Add(_test);
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            bool found = false;

            for (var i = 0; i < _toRemove.Count; i++)
            {

                if (_toRemove[i].Id == id)
                {
                    found = true;
                    _toRemove[i].IsRented = false;
                    _scooters.Insert(0, _toRemove[i]);
                    _toRemove.RemoveAt(i);
                }
            }

            if (!found)
            {
                _scooters.Insert(0, new Scooter(id, pricePerMinute));
            }

        }

        public Scooter RemoveScooter(string id)
        {
            for (var i = 0; i < _scooters.Count; i++)
            {
                if (_scooters[i].Id == id)
                {
                    _scooters[i].IsRented = true;
                    _toRemove.Insert(0, _scooters[i]);
                    _scooters.RemoveAt(i);
                    return _toRemove[0];
                }
            }

            return _scooters[0];
        }

        public IList<Scooter> GetScooters()
        {
            return _scooters;
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

            return null;
        }

    }
}
