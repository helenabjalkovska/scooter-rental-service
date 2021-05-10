using System;

namespace ScooterRentalService
{
    interface IRentedScooters
    {
        string Id { get; set; }
        decimal Price { get; set; }
        DateTime RentStart { get; set; }
        bool IsRented { get; set; }
    }
}
