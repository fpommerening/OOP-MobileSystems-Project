using System;
using System.Collections.Generic;
using System.Linq;
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
            var client = Client();
            var db = client.GetDatabase("PointOfInterestStore");
            var collection = db.GetCollection<PointOfInterest>("PointOfInterests");
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
            var list = new List<PointOfInterest>();

            return list;
        }


        private IMongoClient Client()
        {
            var cnn = DockerSecretHelper.GetSecretValue("DocumentDBCnn");
            return _mongoClient ?? (_mongoClient = new MongoClient(cnn));
        }
    }
}
