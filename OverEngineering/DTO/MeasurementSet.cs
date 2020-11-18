using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OverEngineering.DTO
{
    public class MeasurementSet
    {
        public IEnumerable<Measurement> Temperature { get; set; }
        public IEnumerable<Measurement> Level { get; set; }
    }
}
