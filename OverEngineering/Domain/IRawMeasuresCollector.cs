using OverEngineering.Logic;
using System.Threading.Tasks;

namespace OverEngineering.Domain
{
    public interface IRawMeasuresCollector
    {
        Task<string> CollectRawMeasurement(MeasureType measure);
    }
}
