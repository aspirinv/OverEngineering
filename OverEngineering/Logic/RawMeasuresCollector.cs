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

        public async Task<string> CollectRawMeasurement(MeasureType measure)
        {
            // Retrieving the data
            var response = await _client.GetAsync(_queryBuilder.Build(measure));
            var html = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{measure} gathering failed with. [{response.StatusCode}] {html}");
            }
            return html;
        }
    }
}
