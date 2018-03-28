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

        [BsonElement("externalid")]
        public Guid ExternalId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("createon")]
        public DateTime CreateOn { get; set; }

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("location")]
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }

        [BsonElement("user")]
        public string User { get; set; }


    }
}
