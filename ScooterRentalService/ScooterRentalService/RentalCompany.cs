using System;
using System.Collections.Generic;

namespace ScooterRentalService
{
    public class RentalCompany : IRentalCompany
    {
        ScooterService scooter = new ScooterService();
        private List<Scooter> rentedList = new List<Scooter>();
        public string Name { get; }
        public void StartRent(string id)
        {
            rentedList.Add(scooter.RemoveScooter(id));
        }

        public decimal EndRent(string id)
        {
            for (var i = 0; i < rentedList.Count; i++)
            {
                if (rentedList[i].Id == id)
                {
                    scooter.AddScooter(rentedList[i].Id, rentedList[i].PricePerMinute);
                }
            }

            return 0.0m;
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            throw new NotImplementedException();
        }
    }
}
