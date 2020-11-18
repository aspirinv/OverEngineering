using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OverEngineering.DTO;
using OverEngineering.Logic;

namespace OverEngineering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<MeasurementSet>> Get([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
        {
            // Defaulting values
            from ??= DateTime.Today.AddDays(-1);
            to ??= DateTime.Today;
            // Defining URLs
            var query = $"beginn={from:dd.MM.yyyy}&ende={to:dd.MM.yyyy}";
            using var parser = new MeasureParser(query);
            try
            {
                // Collecting data
                return Ok(new MeasurementSet
                {
                    Temperature = await parser.GetMeasures(MeasureType.Temperature),
                    Level = await parser.GetMeasures(MeasureType.Level),
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
