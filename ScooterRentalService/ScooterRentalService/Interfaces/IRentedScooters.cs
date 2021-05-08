using System;

namespace ScooterRentalService
{
    interface IRentedScooters
    {
        string Name { get; }
        decimal Price { get; }
        DateTime RentStart { get; }
        bool IsRented { get; }
    }
}
