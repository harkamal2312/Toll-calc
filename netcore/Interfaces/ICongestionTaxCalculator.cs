using System;

namespace congestion.calculator.Interfaces
{
    public interface ICongestionTaxCalculator
    {
        int GetTax(string vehicle, DateTime[] dates);
    }
}