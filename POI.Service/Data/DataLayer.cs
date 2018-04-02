using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace POI.Service.Data
{
    public class DataLayer
    {
        private IMongoClient _mongoClient;

        public async Task<string> SavePointOfInterest(PointOfInterest poi)
        {
            var collection = Database().GetCollection<PointOfInterest>("PointOfInterests");
            if (poi.Id == ObjectId.Empty)
            {
                await collection.InsertOneAsync(poi);
            }
            else
            {
                var filter = Builders<PointOfInterest>.Filter.Eq(s => s.Id, poi.Id);
                await collection.ReplaceOneAsync(filter, poi);
            }

            return poi.Id.ToString();
        }

        public async Task<List<PointOfInterest>> GetPointsOfInterest(int latitude, int longtitude)
        {
            var resultList = new List<PointOfInterest>();

            var geo = new GeoJson2DGeographicCoordinates((double)latitude / 100000,
                (double)longtitude / 100000);

            var collection = Database().GetCollection<PointOfInterest>("PointOfInterests");
            var builder = Builders<PointOfInterest>.Filter;

            var filter = builder.NearSphere(x => x.Location, new GeoJsonPoint<GeoJson2DGeographicCoordinates>(geo), 1000);

            using (var filterResult = collection.FindSync(filter))
            {
                while (filterResult.MoveNext())
                {
                    resultList.AddRange(filterResult.Current);
                }
            }
            return resultList;
        }



        private IMongoDatabase Database()
        {
            if (_mongoClient == null)
            {
                var cnn = DockerSecretHelper.GetSecretValue("DocumentDBCnn");
                _mongoClient = new MongoClient(cnn);
            }
            return _mongoClient.GetDatabase("PointOfInterestStore"); ;
        }
    
    }
}
