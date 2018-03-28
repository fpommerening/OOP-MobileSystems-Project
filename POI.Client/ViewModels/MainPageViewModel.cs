using System;
using System.Collections.ObjectModel;
using System.Linq;
using POI.Client.Data;
using Xamarin.Forms;

namespace POI.Client.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly LocalDataRepository _dataRepository;
        private decimal _longtitude = Decimal.Zero;
        private decimal _latitude = Decimal.Zero;

        public MainPageViewModel(INavigation navigation, LocalDataRepository dataReposity)
        {
            _navigation = navigation;
            _dataRepository = dataReposity;
            CreatePoICommand = new Command(CreatePoI, CanCreatePoI);
            GetLocationCommand = new Command(GetLocation);
            FillListCommand = new Command(FillList, CanFillList);
            OpenSettingsCommand = new Command(OpenSettings);
            OpenMyPoIListCommand = new Command(OpenMyPoIList);
            PointsOfInterest = new ObservableCollection<PointOfInterestListItemViewModel>();
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
            return Longtitude != Decimal.Zero && Latitude != Decimal.Zero;
        }

        private void GetLocation()
        {
            Longtitude = DateTime.Now.Second;
            Latitude = DateTime.Now.Second - 10;
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
                    Latitude = poi.Latitude / 100000,
                    Longtitude = poi.Longtitude / 100000,
                    Subject = poi.Name,
                    CreateOn = poi.CreateOn
                });
            }
        }

        private bool CanFillList()
        {
            return Longtitude != Decimal.Zero && Latitude != Decimal.Zero;
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
