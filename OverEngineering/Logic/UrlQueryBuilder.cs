using System;

namespace OverEngineering.Logic
{
    public class UrlQueryBuilder
    {
        public DateTime From { get; set; } = DateTime.Today.AddDays(-1);
        public DateTime To { get; set; } = DateTime.Today;

        public string Build(MeasureType measure)
        {
            var query = $"beginn={From:dd.MM.yyyy}&ende={To:dd.MM.yyyy}";
            var apiName = measure switch
            {
                MeasureType.Temperature => "wassertemperatur",
                MeasureType.Level => "wasserstand",
                _ => throw new NotImplementedException($"Measure type {measure} not implemented")
            };
            return $"{apiName}/isar/muenchen-tieraerztl-hochschule-16516008/messwerte/tabelle?{query}";
        }
    }
}
