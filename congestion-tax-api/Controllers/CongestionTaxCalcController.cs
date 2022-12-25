using congestion.calculator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace congestion_tax_api.Controllers
{
    [ApiController]
    [Route("api/Congestion-Tax-Calc")]
    public class CongestionTaxCalcController : ControllerBase
    {
        private readonly ICongestionTaxCalculator _taxCalc;

        public CongestionTaxCalcController(ICongestionTaxCalculator taxCalc)
        {
            _taxCalc = taxCalc;
        }

        [HttpPost]
        public IActionResult Post([FromBody] VehicleInfo vehicleInfo)
        {
            return Ok(_taxCalc.GetTax(vehicleInfo.Vehicle, vehicleInfo.Dates));
        }
    }
}
