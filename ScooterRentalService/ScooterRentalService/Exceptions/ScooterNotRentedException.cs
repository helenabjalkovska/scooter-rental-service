using System;

namespace ScooterRentalService
{
    public class ScooterNotRentedException : Exception
    {
        public ScooterNotRentedException() : base("Scooter not rented")
        {

        }
    }
}
