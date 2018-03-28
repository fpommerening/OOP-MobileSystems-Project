using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using POI.Client.Droid.Geolocation;
using POI.Client.Geolocation;

[assembly: Xamarin.Forms.Dependency(typeof(LocationService))]
namespace POI.Client.Droid.Geolocation
{
    public class LocationService : IGeolocationService
    {
        private readonly IGeolocator _locator;

        public LocationService()
        {
            _locator = CrossGeolocator.Current;
            _locator.DesiredAccuracy = 100;
        }

        public bool IsGeolocationAvailable
        {
            get { return _locator.IsGeolocationAvailable; }
        }

        public bool IsGeolocationEnabled
        {
            get { return _locator.IsGeolocationEnabled; }
        }

        public async Task<POI.Client.Geolocation.Position> GetLastKnownLocationAsync()
        {
            try
            {
                var pos = await _locator.GetLastKnownLocationAsync();
                return pos == null ? null : MapPostion(pos);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Unable to get location: " + e);
            }
            return null;
        }

        public async Task<POI.Client.Geolocation.Position> GetPositionAsync()
        {
            try
            {
                var pos = await _locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
                return pos == null ? null : MapPostion(pos);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Unable to get location: " + e);
            }

            return null;
        }

        private Client.Geolocation.Position MapPostion(Plugin.Geolocator.Abstractions.Position position)
        {
            return new Client.Geolocation.Position
            {
                Latitude = position.Latitude,
                Longtitude = position.Longitude,
            };
        }
    }
}