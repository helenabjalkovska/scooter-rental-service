using System;

namespace ScooterRentalService
{
    public class ScooterRentedException : Exception
    {
        public ScooterRentedException() : base("Scooter rented")
        {

        }
    }
}
