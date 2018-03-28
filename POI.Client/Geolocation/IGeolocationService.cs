using System.Threading.Tasks;

namespace POI.Client.Geolocation
{
    public interface IGeolocationService
    {
        bool IsGeolocationAvailable { get; }

        bool IsGeolocationEnabled { get; }

        Task<Position> GetLastKnownLocationAsync();

        Task<Position> GetPositionAsync();

    }
}
