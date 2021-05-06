using ScooterRentalService.Models;
using System.Collections.Generic;

namespace ScooterRentalService.Interfaces
{
    public interface IScooterService
    {
        void AddScooter(string id, decimal pricePerMinute);

        Scooter RemoveScooter(string id);

        IList<Scooter> GetScooters();

        Scooter GetScooterById(string scooterId);
    }
}
