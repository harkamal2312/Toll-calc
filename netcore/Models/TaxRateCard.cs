using System;

namespace congestion.calculator.Models
{
    public class TaxRateCard
    {
        public string City { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
        public int Amount { get; set; }
    }
}
