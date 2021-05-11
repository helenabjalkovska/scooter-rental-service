using System;

namespace ScooterRentalService
{
    public class ZeroPriceException : Exception
    {
        public ZeroPriceException() : base("Price can't be zero")
        {

        }
    }
}
