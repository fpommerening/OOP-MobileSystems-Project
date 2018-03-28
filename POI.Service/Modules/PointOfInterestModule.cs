using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.GeoJsonObjectModel;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace POI.Service.Modules
{
    public class PointOfInterestModule : NancyModule
    {
        public PointOfInterestModule()
        {
            this.RequiresAuthentication();

            Put("/api/poi", async (args, ct) =>
            {
                var dl = new Data.DataLayer();
                var dto = this.Bind<Contracts.PointOfInterest>();

                var geo = new GeoJson2DGeographicCoordinates((double) dto.Latitude / 100000,
                    (double) dto.Longtitude / 100000);

                var dbo = new Data.PointOfInterest
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Location = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(geo),
                    CreateOn = dto.CreateOn,
                    Timestamp = DateTime.UtcNow,
                    User = dto.User,
                    ExternalId = dto.Id
                };
                
                await dl.SavePointOfInterest(dbo);
                return HttpStatusCode.Created;
            });

            Get("/api/poi/{latitude}/{longtitude}", async (args, ct) =>
            {
                var dl = new Data.DataLayer();

                var latitude = int.Parse(args.latitude);
                var longtitude = int.Parse(args.longtitude);

                List<Data.PointOfInterest> dbo = await dl.GetPointsOfInterest(latitude, longtitude);
                var dto = dbo.Select(MapToDto).ToArray();
                return Response.AsJson(dto);
            });
        }

        private static Contracts.PointOfInterest MapToDto(Data.PointOfInterest poi)
        {
            var result = new Contracts.PointOfInterest
            {
                CreateOn = poi.CreateOn,
                Description = poi.Description,
                Name = poi.Name,
                User = poi.User,
                Id = poi.ExternalId
            };

            result.Longtitude = (int) Math.Ceiling(poi.Location.Coordinates.Longitude * 100000);
            result.Latitude = (int) Math.Ceiling(poi.Location.Coordinates.Latitude * 100000);
            return result;
        }
    }
}
