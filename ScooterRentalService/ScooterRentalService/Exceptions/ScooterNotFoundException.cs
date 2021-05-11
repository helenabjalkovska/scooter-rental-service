using System;

namespace ScooterRentalService
{
    public class ScooterNotFoundException : Exception
    {
        public ScooterNotFoundException() : base("Scooter not found")
        {

        }
    }
}
