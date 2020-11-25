using System.Threading.Tasks;

namespace OverEngineering.Domain
{
    public interface IRawMeasuresCollector
    {
        Task<string> CollectRawLevel();
        Task<string> CollectRawTemperature();
        Task<string> CollectRawPressure();
        Task<string> CollectRawAirTemperature();
    }
}
