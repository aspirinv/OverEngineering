using OverEngineering.Domain;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OverEngineering.Logic
{
    public class RawMeasuresCollector: IRawMeasuresCollector
    {
        private readonly UrlQueryBuilder _queryBuilder;
        private readonly HttpClient _client;

        public RawMeasuresCollector(HttpClient client, UrlQueryBuilder builder)
        {
            _queryBuilder = builder;
            _client = client;
        }

        public Task<string> CollectRawTemperature()
            => CollectRawMeasurement(_queryBuilder.BuildTemperatureQuery());
        public Task<string> CollectRawLevel()
            => CollectRawMeasurement(_queryBuilder.BuildLevelQuery());

        private async Task<string> CollectRawMeasurement(string path)
        {
            // Retrieving the data
            var response = await _client.GetAsync(path);
            var html = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{path} gathering failed with. [{response.StatusCode}] {html}");
            }
            return html;
        }
    }
}
