using System.Collections.Generic;

namespace ScooterRentalService
{
    public class ScooterService : IScooterService
    {
        private Scooter test = new Scooter("test", 1.1m);
        private List<Scooter> scooters = new List<Scooter>();
        private List<Scooter> toRemove = new List<Scooter>();

        public void AddScooter(string id, decimal pricePerMinute)
        {
            scooters.Add(new Scooter(id, pricePerMinute));
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

            return test;
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
