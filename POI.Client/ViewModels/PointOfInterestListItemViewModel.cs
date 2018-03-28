using System;

namespace POI.Client.ViewModels
{ 
    public class PointOfInterestListItemViewModel : BaseViewModel
    {
        private bool _transmitted;

        public decimal Latitude { get; set; }

        public decimal Longtitude { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime CreateOn { get; set; }

        public bool Transmitted
        {
            get => _transmitted;
            set
            {
                _transmitted = value;
                OnPropertyChanged();
            }
        }
    }
}




