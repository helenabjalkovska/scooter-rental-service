using System.Collections.Generic;

namespace ScooterRentalService
{
    public class ScooterService : IScooterService
    {
        private List<Scooter> scooters = new List<Scooter>();

        public void AddScooter(string id, decimal pricePerMinute)
        {
            scooters.Add(new Scooter(id, pricePerMinute));
        }

        public void RemoveScooter(string id)
        {
            for (var i = 0; i < scooters.Count; i++)
            {
                if (scooters[i].Id == id)
                {
                    scooters.RemoveAt(i);
                }
            }
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
