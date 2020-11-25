using System;

namespace OverEngineering.Logic
{
    public class UrlQueryBuilder
    {
        public DateTime From { get; set; } = DateTime.Today.AddDays(-1);
        public DateTime To { get; set; } = DateTime.Today;

        public string BuildTemperatureQuery() => Build("de/fluesse/wassertemperatur");
        public string BuildLevelQuery() => Build("de/fluesse/wasserstand");
        public string BuildPressureQuery() => Build("de/fluesse/abfluss");
        public string BuildAirTemperatureQuery() 
            => $"/de/meteo/lufttemperatur/isar/eichenried-200114/messwerte/tabelle{Query()}";

        private string Build(string apiName)
            =>  $"{apiName}/isar/muenchen-tieraerztl-hochschule-16516008/messwerte/tabelle{Query()}";
        private string Query() => $"?beginn={From:dd.MM.yyyy}&ende={To:dd.MM.yyyy}";
    }
}
