using POI.Client.Data;
using Xamarin.Forms;

namespace POI.Client.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly ILocalDataRepository _dataRepository;
        private readonly INavigation _navigation;

        private string _serviceUrl;
        private string _user;

        public SettingsViewModel(ILocalDataRepository dataRepository, INavigation navigation)
        {
            _dataRepository = dataRepository;
            _navigation = navigation;

            _serviceUrl = _dataRepository.Configuration.ServiceUrl;
            _user = _dataRepository.Configuration.User;
            SaveCommand = new Command(Save);
        }

        public string ServiceUrl
        {
            get => _serviceUrl;
            set
            {
                _serviceUrl = value; 
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

        public Command SaveCommand { get; set; }

        public async void Save()
        {
            _dataRepository.Configuration.User = User;
            _dataRepository.Configuration.ServiceUrl = ServiceUrl;
            await _dataRepository.Save();
            await _navigation.PopAsync(true);
        }
    }
}
