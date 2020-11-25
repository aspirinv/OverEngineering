using OverEngineering.Domain;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OverEngineering.Logic
{
    public class RawMeasuresCollector: IRawMeasuresCollector, IDisposable
    {
        private static Uri BaseUrl = new Uri("https://www.gkd.bayern.de/de/fluesse/");
        private readonly string _query;
        private readonly HttpClient _client;

        public RawMeasuresCollector(string query)
        {
            _query = query;
            _client = new HttpClient { BaseAddress = BaseUrl };
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task<string> CollectRawMeasurement(MeasureType measure)
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
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{apiName} gathering failed with. [{response.StatusCode}] {html}");
            }
            return html;
        }
    }
}
