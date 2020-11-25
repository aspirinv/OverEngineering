using System;
using System.Globalization;

namespace OverEngineering.DTO
{
    public class Measurement
    {
        private static CultureInfo DeCulture = new CultureInfo("de-DE");
        public DateTime Date { get; set; }
        public decimal Value { get; set; }

        public static Measurement Parse(string date, string value)
        {
            if (DateTime.TryParseExact(date, "dd.MM.yyyy HH:mm", DeCulture, DateTimeStyles.None, out var d) 
                && decimal.TryParse(value, NumberStyles.Float, DeCulture, out var v))
            {
                return new Measurement { Date = d, Value = v };
            }            
            return null;
        }
    }
}
