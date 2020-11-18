using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OverEngineering.DTO;

namespace OverEngineering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<MeasurementSet>> Get([FromQuery]DateTime? from = null, [FromQuery]DateTime? to = null)
        {
            // Defaulting values
            from ??= DateTime.Today.AddDays(-1);
            to ??= DateTime.Today;
            // Defining URLs
            var query = $"beginn={from:dd.MM.yyyy}&ende={to:dd.MM.yyyy}";
            var baseUrl = new Uri("https://www.gkd.bayern.de/de/fluesse/");
            using (var client = new HttpClient { BaseAddress = baseUrl })
            {
                // Collecting data
                return Ok(new MeasurementSet 
                { 
                    Temperature = await GetMeasures(query, client, "wassertemperatur"), 
                    Level = await GetMeasures(query, client, "wasserstand"), 
                });
            }
        }

        private static async Task<IEnumerable<Measurement>> GetMeasures(string query, HttpClient client, string apiName)
        {
            // Retrieving the data
            var response = await client.GetAsync($"{apiName}/isar/muenchen-tieraerztl-hochschule-16516008/messwerte/tabelle?{query}");
            var html = await response.Content.ReadAsStringAsync();
            // Parsing HTML response
            var bodyMatch = Regex.Match(html, "<tbody>(.*)<\\/tbody>");
            var rowsHtml = bodyMatch.Groups.Values.Last();
            return Regex.Matches(rowsHtml.Value, "<tr  class=\"row2?\"><td >([^<]*)<\\/td><td  class=\"center\">([^<]*)<\\/td>")
                // Building the results
                .Select(match => new Measurement
                {
                    Date = DateTime.Parse(match.Groups[1].Value),
                    Value = decimal.Parse(match.Groups[2].Value)
                });
        }
    }
}
