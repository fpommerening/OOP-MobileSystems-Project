using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using POI.Contracts;

namespace POI.Client.Data
{
    public class ServiceClient
    {
        private readonly string _serviceUri;
        private readonly string _servicePasswort;

        public ServiceClient(string url)
        {
            _serviceUri = url;
            _servicePasswort = $"mediaproject-{DateTime.Now:yyyyMMdd}";
        }

        public async Task<PointOfInterest[]> GetPointsOfInterest(int latitude, int longtitude)
        {
            using (var client = GetServiceClient())
            {
                var url = $"{_serviceUri}/api/poi/{latitude}/{longtitude}";
                using (var response = await client.GetAsync(url))
                {
                   var msg = response.EnsureSuccessStatusCode();
                    var contentText = await msg.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PointOfInterest[]>(contentText);
                }
            }
        }

        public async Task SavePointOfInterest(PointOfInterest poi)
        {
            using (var client = GetServiceClient())
            {
                var url = $"{_serviceUri}/api/poi/";
                var json = JsonConvert.SerializeObject(poi);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                using (var response = await client.PutAsync(url, stringContent))
                {
                   response.EnsureSuccessStatusCode();
                }
            }
        }

        private HttpClient GetServiceClient()
        {
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes($"POIService:{_servicePasswort}");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            return client;
        }
    }
}
