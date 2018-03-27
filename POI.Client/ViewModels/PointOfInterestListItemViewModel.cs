using System;

namespace POI.Client.ViewModels
{ 
    public class PointOfInterestListItemViewModel : BaseViewModel
    {
        public decimal Latitude { get; set; }

        public decimal Longtitude { get; set; }

        public string Subject { get; set; }

        public DateTime CreateOn { get; set; }
    }
}




