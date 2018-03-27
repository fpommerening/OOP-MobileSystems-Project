using System;
using POI.Client.Data.FileSystem;
using Xamarin.Forms;

namespace POI.Client.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private INavigation _navigation;
        private IPathService _pathService;
        private decimal _longtitude = Decimal.Zero;
        private decimal _latitude = Decimal.Zero;

        public MainPageViewModel(INavigation navigation, IPathService pathService)
        {
            _navigation = navigation;
            _pathService = pathService;
            CreatePoICommand = new Command(CreatePoI, CanCreatePoI);
            GetLocationCommand = new Command(GetLocation);
            FillListCommand = new Command(FillList, CanFillList);
        }

        public decimal Latitude
        {
            get => _latitude;
            set
            {
                _latitude = value;
                OnPropertyChanged();
                CreatePoICommand.ChangeCanExecute();
                FillListCommand.ChangeCanExecute();
            }
        }

        public decimal Longtitude
        {
            get => _longtitude;
            set
            {
                _longtitude = value;
                OnPropertyChanged();
                CreatePoICommand.ChangeCanExecute();
                FillListCommand.ChangeCanExecute();
            }
        }

        public Command CreatePoICommand { get; set; }

        public Command GetLocationCommand { get; set; }

        public Command FillListCommand { get; set; }

        private void CreatePoI()
        {
            var poiViewModel = new PointOfInterestViewModel(_navigation, _pathService)
            {
                Longtitude = this.Longtitude,
                Latitude = this.Latitude
            };

            var poi = new PointOfInterest {ViewModel = poiViewModel};
            _navigation.PushAsync(poi);
        }

        private bool CanCreatePoI()
        {
            return Longtitude != Decimal.Zero && Latitude != Decimal.Zero;
        }

        private void GetLocation()
        {
            Longtitude = DateTime.Now.Second;
            Latitude = DateTime.Now.Second - 10;
        }

        private void FillList()
        {

        }

        private bool CanFillList()
        {
            return Longtitude != Decimal.Zero && Latitude != Decimal.Zero;
        }


    }
}
