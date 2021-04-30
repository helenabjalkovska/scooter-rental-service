using System.Collections.Generic;

namespace ScooterRentalService
{
    public class ScooterService : IScooterService
    {
        private Scooter test;
        private List<Scooter> scooters;
        private List<Scooter> toRemove;

        public ScooterService()
        {
            scooters = new List<Scooter>();
            toRemove = new List<Scooter>();
            test = new Scooter("test", 1.1m);

            scooters.Add(test);
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            scooters.Insert(0, new Scooter(id, pricePerMinute));
        }

        public Scooter RemoveScooter(string id)
        {
            for (var i = 0; i < scooters.Count; i++)
            {
                if (scooters[i].Id == id)
                {
                    toRemove.Add(scooters[i]);
                    scooters.RemoveAt(i);
                    return toRemove[0];
                }
            }

            return scooters[0];
        }

        public IList<Scooter> GetScooters()
        {
            return scooters;
        }

        public Scooter GetScooterById(string scooterId)
        {
            for (var i = 0; i < scooters.Count; i++)
            {
                if (scooters[i].Id == scooterId)
                {
                    return scooters[i];
                }
            }

            return null;
        }

    }
}
