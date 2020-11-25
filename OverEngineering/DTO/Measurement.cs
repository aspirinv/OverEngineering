using System;

namespace OverEngineering.DTO
{
    public class Measurement
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }

        public static Measurement Parse(string date, string value)
        {
            if (DateTime.TryParse(date, out var d) && decimal.TryParse(value, out var v))
            {
                return new Measurement { Date = d, Value = v };
            }
            return null;
        }
    }
}
