using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using POI.Client.Data.Models;

namespace POI.Client.Data
{
    public class LocalDataRepository
    {
        private readonly string _configurationPath;
        private readonly string _pointsPath;

        public LocalDataRepository(string localPath)
        {
            _configurationPath = Path.Combine(localPath, "configuration.json");
            _pointsPath = Path.Combine(localPath, "points.json");
        }

        public Configuration Configuration { get; set; } = new Configuration();

        public List<PointOfInterest> PointOfInterestList { get; private set; } = new List<PointOfInterest>();

        public async Task Load()
        {
            if (File.Exists(_configurationPath))
            {

                var configData = await FileHelper.ReadAllTextAsync(_configurationPath, Encoding.UTF8);
                Configuration = JsonConvert.DeserializeObject<Configuration>(configData);
            }

            if (File.Exists(_pointsPath))
            {
                var pointsData = await FileHelper.ReadAllTextAsync(_pointsPath, Encoding.UTF8);
                PointOfInterestList = JsonConvert.DeserializeObject<PointOfInterest[]>(pointsData).ToList();
            }
        }

        public async Task Save()
        {
            var configData = JsonConvert.SerializeObject(this.Configuration);
            await FileHelper.WriteAllTextAsync(_configurationPath, configData, Encoding.UTF8);

            var pointsData = JsonConvert.SerializeObject(this.PointOfInterestList.ToArray());
            await FileHelper.WriteAllTextAsync(_pointsPath, pointsData, Encoding.UTF8);
        }
     
    }
}
