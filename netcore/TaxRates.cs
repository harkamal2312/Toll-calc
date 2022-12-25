using congestion.calculator.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace congestion.calculator
{
    internal class TaxRates
    {
        static TaxRates()
        {
            var path = Path.Combine(Environment.CurrentDirectory,"Data\\", "RateCard.json");
            using var streamReader = new StreamReader(path);
            var json = streamReader.ReadToEnd();
            LstTaxRates = JsonConvert.DeserializeObject<TaxRateCard[]>(json);
        }

        public static TaxRateCard[] LstTaxRates { get; }
    }
}
