using System;

namespace POI.Contracts
{
    public class PointOfInterest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateOn { get; set; }

        public int Latitude { get; set; }

        public int Longtitude { get; set; }

        public string User { get; set; }
    }
}
