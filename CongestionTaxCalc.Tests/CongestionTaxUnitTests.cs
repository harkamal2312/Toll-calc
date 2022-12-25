using NUnit.Framework;
using System;
using congestion.calculator;

namespace CongestionTaxCalc.Tests
{
    public class Tests
    {
        private CongestionTaxCalculator _congestionTaxCalculator;

        [SetUp]
        public void Setup()
        {
            _congestionTaxCalculator = new CongestionTaxCalculator();
        }
        [Test]
        [TestCase("Car")]
        public void TestCongestionTaxForCarSingleToll(string vehicle)
        {

            var dates = new DateTime[1];
            dates[0] = Convert.ToDateTime("2013-03-22 14:07:27");
            var result = _congestionTaxCalculator.GetTax(vehicle, dates);
            Assert.AreEqual(8, result);
        }
        [Test]
        [TestCase("Car")]
        public void TestCongestionTaxForCarMultipleTolls(string vehicle)
        {

            var dates = new DateTime[2];
            dates[0] = Convert.ToDateTime("2013-02-07 06:23:27");
            dates[1] = Convert.ToDateTime("2013-02-07 15:27:00");
            var result = _congestionTaxCalculator.GetTax(vehicle, dates);
            Assert.AreEqual(21, result);
        }

        [Test]
        [TestCase("Car")]
        public void TestCongestionTaxForCarMultipleTollsWithinOneHour(string vehicle)
        {
            var dates = new DateTime[5];
            dates[0] = Convert.ToDateTime("2013-02-08 06:27:00");
            dates[1] = Convert.ToDateTime("2013-02-08 06:20:27");
            dates[2] = Convert.ToDateTime("2013-02-08 14:35:00");
            dates[3] = Convert.ToDateTime("2013-02-08 15:29:00");
            dates[4] = Convert.ToDateTime("2013-02-08 18:35:00");
            var result = _congestionTaxCalculator.GetTax(vehicle, dates);
            Assert.AreEqual(21, result);
        }
        [Test]
        [TestCase("Foreign")]
        public void TestCongestionTaxForTollExemptedVehicle(string vehicle)
        {
            var dates = new DateTime[1];
            dates[0] = Convert.ToDateTime("2013-02-08 16:01:00");
            var result = _congestionTaxCalculator.GetTax(vehicle, dates);
            Assert.AreEqual(0, result);
        }
        [Test]
        [TestCase("Car")]
        public void TestCongestionTaxForWeekend(string vehicle)
        {
            var dates = new DateTime[1];
            dates[0] = Convert.ToDateTime("2013-02-10 11:15:00");        //Sunday
            var result = _congestionTaxCalculator.GetTax(vehicle, dates);
            Assert.AreEqual(0, result);
        }
    }
}