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

        private void CreatePoI()
        {
            var poiViewModel = new PointOfInterestViewModel(_navigation, _dataRepository)
            {
                Longtitude = this.Longtitude,
                Latitude = this.Latitude
            };
            var poi = new PointOfInterest(poiViewModel);
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
            //var sc = new ServiceClient(_dataRepository.Configuration.ServiceUrl);
            //var dto = await sc.GetPointsOfInterest((int) Latitude * 100000, (int) Longtitude * 100000);

          var bla = _dataRepository.PointOfInterestList;
            PointsOfInterest.Clear();

            if (!bla.Any()) return;
            foreach (var b in bla)
            {
                var p = new PointOfInterestListItemViewModel
                {
                    Latitude = b.Latitude,
                    Longtitude = b.Longtitude,
                    Subject = b.Name,
                    CreateOn = b.CreateOn
                };
                PointsOfInterest.Add(p);
            }

        }

        private bool CanFillList()
        {
            return Longtitude != Decimal.Zero && Latitude != Decimal.Zero;
        }
    }
}
