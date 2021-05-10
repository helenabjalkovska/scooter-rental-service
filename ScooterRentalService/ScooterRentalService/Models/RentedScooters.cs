using System;

namespace ScooterRentalService
{
    class RentedScooters : IRentedScooters
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public DateTime RentStart { get; set; }
        public bool IsRented { get; set; }

        public RentedScooters(string aId, decimal aPrice, DateTime aRentTime, bool aIsRented)
        {
            Id = aId;
            Price = aPrice;
            RentStart = aRentTime;
            IsRented = aIsRented;
        }
    }
}
