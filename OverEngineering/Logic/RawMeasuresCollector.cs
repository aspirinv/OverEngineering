using OverEngineering.Domain;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OverEngineering.Logic
{
    public class RawMeasuresCollector: IRawMeasuresCollector, IDisposable
    {
        private readonly UrlQueryBuilder _queryBuilder;
        private readonly HttpClient _client;

        public RawMeasuresCollector(IHttpClientFactory clientFactory)
        {
            _queryBuilder = new UrlQueryBuilder();
            _client = clientFactory.CreateClient(Constants.ClientName);
        }

        public void SetFrom(DateTime? from)
        {
            if (from.HasValue)
                _queryBuilder.From = from.Value;
        }
        public void SetTo(DateTime? to)
        {
            if (to.HasValue)
                _queryBuilder.To = to.Value;
        }
        public void Dispose()
        {
            _client.Dispose();
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
