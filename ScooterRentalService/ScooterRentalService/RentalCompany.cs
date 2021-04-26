using System;

namespace ScooterRentalService
{
    class RentalCompany : IRentalCompany
    {
        public string Name { get; }
        public void StartRent(string id)
        {
            throw new NotImplementedException();
        }

        public decimal EndRent(string id)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            throw new NotImplementedException();
        }
    }
}
