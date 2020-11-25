using OverEngineering.Domain;
using OverEngineering.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OverEngineering.Logic
{
    public class MeasureParser 
    {
        private readonly IRawMeasuresCollector _collector;

        public MeasureParser(IRawMeasuresCollector collector)
        {
            _collector = collector;
        }

        public async Task<IEnumerable<Measurement>> GetTemperature() 
            => GetMeasures(await _collector.CollectRawTemperature());
        public async Task<IEnumerable<Measurement>> GetLevel() 
            => GetMeasures(await _collector.CollectRawLevel());
        public async Task<IEnumerable<Measurement>> GetPressure()
            => GetMeasures(await _collector.CollectRawPressure());
        public async Task<IEnumerable<Measurement>> GetAirTemperature()
            => GetMeasures(await _collector.CollectRawAirTemperature());

        private IEnumerable<Measurement> GetMeasures(string html)
        {
            // Parsing HTML response
            var bodyMatch = Regex.Match(html, "<tbody>(.*)<\\/tbody>");
            if (!bodyMatch.Success)
            {
                throw new Exception($"Failed to define data table body. Content: {html}");
            }

            var rowsHtml = bodyMatch.Groups.Values.Last();
            return Regex.Matches(rowsHtml.Value, "<tr  class=\"row2?\"><td >([^<]*)<\\/td><td  class=\"center\">([^<]*)<\\/td>")
                // Building the results
                .Select(match => Measurement.Parse(match.Groups[1].Value, match.Groups[2].Value))
                // Ignoring invalid values
                .Where(m => m != null);
        }
    }
}
