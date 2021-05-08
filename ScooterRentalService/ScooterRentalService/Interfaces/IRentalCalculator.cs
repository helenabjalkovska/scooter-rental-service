using System;
using System.Collections.Generic;

namespace ScooterRentalService
{
    interface IRentalCalculator
    {
        decimal CalculateRent(DateTime endTime, DateTime startTime, decimal price);
        void SaveIncome(DateTime time, decimal payment, Dictionary<int, decimal> incomeDictionary);
    }
}
