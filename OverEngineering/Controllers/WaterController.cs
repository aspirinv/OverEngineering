using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OverEngineering.Domain;
using OverEngineering.DTO;
using OverEngineering.Logic;

namespace OverEngineering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterController : ControllerBase
    {
        private readonly RawMeasuresCollectorFactory _collectorFactory;

        public WaterController(RawMeasuresCollectorFactory collectorFactory)
        {
            _collectorFactory = collectorFactory;
        }

        [HttpGet]
        public async Task<ActionResult<MeasurementSet>> Get([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
        {
            var parser = new MeasureParser(_collectorFactory.CreateCollector(from, to));
            return Ok(new MeasurementSet
            {
                Temperature = await parser.GetTemperature(),
                Level = await parser.GetLevel(),
                Pressure = await parser.GetPressure(),
                AirTemperature = await parser.GetAirTemperature(),
            });
        }
    }
}
