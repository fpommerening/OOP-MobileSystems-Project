using System.Collections.Generic;
using System.Threading.Tasks;
using POI.Client.Data.Models;

namespace POI.Client.Data
{
    public interface ILocalDataRepository
    {
        Configuration Configuration { get; set; }
        List<PointOfInterest> PointOfInterestList { get; }
        Task Load();
        Task Save();
    }
}