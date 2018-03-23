using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace POI.Service.Data
{
    public class PointOfInterest
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("name")]
        public string Description { get; set; }

        [BsonElement("createon")]
        public DateTime CreateOn { get; set; }

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("latitude")]
        public int Latitude { get; set; }

        public GeoJsonPoint<GeoJson2DGeographicCoordinates> loc { get; set; }

        [BsonElement("longtitude")]
        public int Longtitude { get; set; }

        [BsonElement("user")]
        public string User { get; set; }


    }
}
