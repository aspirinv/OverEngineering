using OverEngineering.Logic;
using System;
using System.Threading.Tasks;

namespace OverEngineering.Domain
{
    public interface IRawMeasuresCollector
    {
        Task<string> CollectRawMeasurement(MeasureType measure);
        void SetFrom(DateTime? from);
        void SetTo(DateTime? to);
    }
}
