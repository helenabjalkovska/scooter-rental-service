using System;

namespace ScooterRentalService
{
    public class ScooterNotFoundException : Exception
    {
        public ScooterNotFoundException() : base("Scooter not found")
        {

        }

        public ScooterNotFoundException(string message) : base(message)
        {

        }
    }
}
