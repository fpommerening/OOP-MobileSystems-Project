using POI.Client.Data;
using POI.Client.Data.FileSystem;
using Xamarin.Forms;

namespace POI.Client.ViewModels
{
    public class PointOfInterestViewModel : BaseViewModel
    {
        private INavigation _navigation;
        private IPathService _pathService;
        private decimal _latitude;
        private decimal _longtitude;
        private string _subject;
        private string _description;
        private string _createOn;
        private string _user;
        private bool _transmitted;

        public PointOfInterestViewModel(INavigation navigation, IPathService pathService)
        {
            _navigation = navigation;
            _pathService = pathService;
            SaveCommand = new Command(Save, CanSave);
        }

        public decimal Latitude
        {
            get => _latitude;
            set
            {
                _latitude = value; 
                OnPropertyChanged();
            }
        }

        public decimal Longtitude
        {
            get => _longtitude;
            set
            {
                _longtitude = value;
                OnPropertyChanged();
            }
        }

        public string Subject
        {
            get => _subject;
            set
            {
                _subject = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public string CreateOn
        {
            get => _createOn;
            set
            {
                _createOn = value;
                OnPropertyChanged();
            }
        }

        public string User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        public bool Transmitted
        {
            get => _transmitted;
            set
            {
                _transmitted = value;
                OnPropertyChanged();
            }
        }

        public Command SaveCommand { get; set; }

        private void Save()
        {
            _navigation.PopAsync();

            //var localDataRepo = new LocalDataReposity(_pathService.GetDataPath());
            //localDataRepo.Load().


        }

        private bool CanSave()
        {
            return true;
        }
    }
}
