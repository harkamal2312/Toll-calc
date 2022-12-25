using congestion.calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using congestion.calculator.Interfaces;

namespace congestion.calculator
{
    public partial class CongestionTaxCalculator : ICongestionTaxCalculator
    {
        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */

        private static readonly HashSet<string> TaxFreeVehicles = Enum.GetNames(typeof(TollFreeVehicles))
            .Select(x => x.ToLower())
            .ToHashSet();

        public int GetTax(string vehicle, DateTime[] dates)
        {
            if (string.IsNullOrWhiteSpace(vehicle))
            {
                return 0;
            }
            var rates = TaxRates.LstTaxRates;
            var totalFee = 0;
            var intervalStart = dates[0];
            foreach (var date in dates)
            {
                var nextFee = GetTollFee(date, vehicle, rates);
                var diffInMinutes = date.Subtract(intervalStart).TotalMinutes;
                var feeToAdd = nextFee;

                if (diffInMinutes <= 60 && Math.Sign(totalFee) == +1)
                {
                    var tempFee = GetTollFee(intervalStart, vehicle, rates);
                    totalFee -= tempFee;
                    feeToAdd = Math.Max(tempFee, nextFee);
                }

                totalFee += feeToAdd;
                intervalStart = date;
            }

            return Math.Min(totalFee, 60);
        }

        public bool IsTollFreeVehicle(string vehicle)
        {
            return TaxFreeVehicles.Contains(vehicle.ToLower());
        }

        public int GetTollFee(DateTime date, string vehicle, IEnumerable<TaxRateCard> rates)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle))
            {
                return 0;
            }

            return rates
                .FirstOrDefault(rate => IsEligibleForTollFee(date, rate))?
                .Amount ?? 0;
        }

        private static bool IsEligibleForTollFee(DateTime date, TaxRateCard taxRateCard)
        {
            var start = new TimeSpan(taxRateCard.StartHour, taxRateCard.StartMinute, 0);
            var end = new TimeSpan(taxRateCard.EndHour, taxRateCard.EndMinute, 0);
            var currentTime = date.TimeOfDay;
            return start <= currentTime && currentTime <= end;
        }

        public bool IsTollFreeDate(DateTime date)
        {
            if (date.IsWeekend())
            {
                return true;
            }

            if (date.Year != 2013)
            {
                return false;
            }

            return 
                date.IsHoliday() ||
                date.Month == 7;
        }
    }
}
