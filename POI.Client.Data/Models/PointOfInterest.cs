using System;

namespace POI.Client.Data.Models
{
    public class PointOfInterest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateOn { get; set; }

        public int Latitude { get; set; }

        public int Longtitude { get; set; }

        public string User { get; set; }

        public bool Transmitted { get; set; }
    }
}
