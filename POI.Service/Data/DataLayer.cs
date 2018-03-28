using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

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

            var collection = Database().GetCollection<PointOfInterest>("PointOfInterests");
            var builder = Builders<PointOfInterest>.Filter;

            //builder.GeoWithinCenterSphere(x => x.loc, latitude, longtitude, 10);

            //builder.GeoWithinCenterSphere(x => x.loc, latitude, longtitude, 10);

            var filter = builder.Empty;

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
