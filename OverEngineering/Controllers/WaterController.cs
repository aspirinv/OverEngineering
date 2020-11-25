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
        private readonly IRawMeasuresCollector _collector;

        public WaterController(IRawMeasuresCollector collector)
        {
            _collector = collector;
        }


        [HttpGet]
        public async Task<ActionResult<MeasurementSet>> Get([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
        {
            _collector.SetFrom(from);
            _collector.SetTo(to);
            var parser = new MeasureParser(_collector);

            return Ok(new MeasurementSet
            {
                Temperature = await parser.GetMeasures(MeasureType.Temperature),
                Level = await parser.GetMeasures(MeasureType.Level),
            });
        }
    }
}
