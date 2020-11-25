using OverEngineering.Logic;
using System;
using System.Net.Http;

namespace OverEngineering.Domain
{
    public class RawMeasuresCollectorFactory
    {
        private readonly IHttpClientFactory _clientFactory;

        public RawMeasuresCollectorFactory(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IRawMeasuresCollector CreateCollector(DateTime? from, DateTime? to)
        {
            var builder = new UrlQueryBuilder
            {
                From = from ?? DateTime.Today.AddDays(-1),
                To = to ?? DateTime.Today
            };
            return new RawMeasuresCollector(_clientFactory.CreateClient(Constants.ClientName), builder);
        }
    }
}
