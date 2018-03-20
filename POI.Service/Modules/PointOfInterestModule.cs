using System;
using System.Collections.Generic;
using System.Linq;
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

            Put("/api/poi", action: async (args, ct) =>
            {
                var dl = new Data.DataLayer();
                var dto = this.Bind<Contracts.PointOfInterest>();

                var dbo = new Data.PointOfInterest
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Latitude = dto.Latitude,
                    Longtitude = dto.Longtitude,
                    CreateOn = dto.CreateOn,
                    Timestamp = DateTime.UtcNow,
                };
                await dl.SavePointOfInterest(dbo);
                return HttpStatusCode.Created;
            });

            Get("/api/poi/{latitude}/{longtitude}", action: async (args, ct) =>
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
            return new Contracts.PointOfInterest
            {
                CreateOn = poi.CreateOn,
                Description = poi.Description,
                Name = poi.Name,
                Longtitude = poi.Longtitude,
                Latitude = poi.Latitude,
                User = poi.User
            };
        }
    }
}
