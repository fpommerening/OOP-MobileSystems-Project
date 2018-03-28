using System;
using System.Collections.ObjectModel;
using System.Linq;
using POI.Client.Data;
using POI.Client.Geolocation;
using Xamarin.Forms;




namespace POI.Client.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly LocalDataRepository _dataRepository;
        private readonly IGeolocationService _locationService;
        private double _longtitude = Double.NaN;
        private double _latitude = Double.NaN;

        public MainPageViewModel(INavigation navigation, LocalDataRepository dataReposity, IGeolocationService locationService)
        {
            _navigation = navigation;
            _dataRepository = dataReposity;
            _locationService = locationService;
            CreatePoICommand = new Command(CreatePoI, CanCreatePoI);
            GetLocationCommand = new Command(GetLocation, CanGetLocation);
            FillListCommand = new Command(FillList, CanFillList);
            OpenSettingsCommand = new Command(OpenSettings);
            OpenMyPoIListCommand = new Command(OpenMyPoIList);
            PointsOfInterest = new ObservableCollection<PointOfInterestListItemViewModel>();
        }

        public double Latitude
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

        public double Longtitude
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

        public ObservableCollection<PointOfInterestListItemViewModel> PointsOfInterest { get; set; }

        public Command CreatePoICommand { get; set; }

        public Command GetLocationCommand { get; set; }

        public Command FillListCommand { get; set; }

        public Command OpenSettingsCommand { get; set; }

        public Command OpenMyPoIListCommand { get; set; }

        private void CreatePoI()
        {
            var poiViewModel = new PointOfInterestViewModel(_navigation, _dataRepository)
            {
                Longtitude = this.Longtitude,
                Latitude = this.Latitude
            };
            var poi = new PointOfInterest(poiViewModel);
            _navigation.PushAsync(poi, true);
        }

        private bool CanCreatePoI()
        {
            return !double.IsNaN(Longtitude) && !double.IsNaN(Latitude);
        }

        private async void GetLocation()
        {
            Position position = null;

            position = await _locationService.GetPositionAsync();

            if (position == null)
            {
                position = await _locationService.GetLastKnownLocationAsync();
            }

            if (position != null)
            {
                Longtitude = position.Longtitude;
                Latitude = position.Latitude;
            }
            else
            {
                Latitude = double.NaN;
                Longtitude = double.NaN;
            }
        }

        private bool CanGetLocation()
        {
            return _locationService.IsGeolocationAvailable && _locationService.IsGeolocationEnabled;
        }

        private async void FillList()
        {
            var sc = new ServiceClient(_dataRepository.Configuration.ServiceUrl);
            var dto = await sc.GetPointsOfInterest((int) Latitude * 100000, (int) Longtitude * 100000);

            PointsOfInterest.Clear();

            if (!dto.Any()) return;
            foreach (var poi in dto)
            {
                PointsOfInterest.Add(new PointOfInterestListItemViewModel
                {
                    Latitude = (double)poi.Latitude / 100000,
                    Longtitude = (double)poi.Longtitude / 100000,
                    Subject = poi.Name,
                    CreateOn = poi.CreateOn,
                    User = poi.User
                });
            }
        }

        private bool CanFillList()
        {
            return !double.IsNaN(Longtitude) && !double.IsNaN(Latitude);
        }

        public void OpenSettings()
        {
            var settings = new Settings(_dataRepository, _navigation);
            _navigation.PushAsync(settings, true);
        }

        public void OpenMyPoIList()
        {
            var list = new PointOfInterestList(_dataRepository);
            _navigation.PushAsync(list, true);
        }
    }
}
