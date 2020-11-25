using System;

namespace OverEngineering.Logic
{
    public class UrlQueryBuilder
    {
        public DateTime From { get; set; } = DateTime.Today.AddDays(-1);
        public DateTime To { get; set; } = DateTime.Today;

        public string BuildTemperatureQuery() => Build("wassertemperatur");
        public string BuildLevelQuery() => Build("wasserstand");

        private string Build(string apiName)
        {
            var query = $"beginn={From:dd.MM.yyyy}&ende={To:dd.MM.yyyy}";
            return $"{apiName}/isar/muenchen-tieraerztl-hochschule-16516008/messwerte/tabelle?{query}";
        }
    }
}
