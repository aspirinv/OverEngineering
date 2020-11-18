using OverEngineering.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OverEngineering.Logic
{
    public class MeasureParser : IDisposable
    {
        private static Uri BaseUrl = new Uri("https://www.gkd.bayern.de/de/fluesse/");
        private readonly string _query;
        private readonly HttpClient _client;

        public MeasureParser(string query)
        {
            _query = query;
            _client = new HttpClient { BaseAddress = BaseUrl };
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task<IEnumerable<Measurement>> GetMeasures(MeasureType measure)
        {
            var apiName = measure switch
            {
                MeasureType.Temperature => "wassertemperatur",
                MeasureType.Level => "wasserstand",
                _ => throw new NotImplementedException($"Measure type {measure} not implemented")
            };

            // Retrieving the data
            var response = await _client.GetAsync($"{apiName}/isar/muenchen-tieraerztl-hochschule-16516008/messwerte/tabelle?{_query}");
            var html = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                throw new Exception($"{apiName} gathering failed with. [{response.StatusCode}] {html}");
            }
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
