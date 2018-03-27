namespace POI.Client.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private string _serviceUrl;
        private string _user;

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
    }
}
