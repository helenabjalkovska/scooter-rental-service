using System;

namespace ScooterRentalService
{
    public class IncomeNotFoundException : Exception
    {
        public IncomeNotFoundException() : base("Income not found")
        {

        }
    }
}
