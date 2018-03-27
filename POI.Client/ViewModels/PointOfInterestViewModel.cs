using System;
using POI.Client.Data;
using Xamarin.Forms;

namespace POI.Client.ViewModels
{
    public class PointOfInterestViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly LocalDataRepository _dataRepository;
        private decimal _latitude;
        private decimal _longtitude;
        private string _subject;
        private string _description;
        private DateTime _createOn;
        private string _user;
        private bool _transmitted;

        public PointOfInterestViewModel(INavigation navigation, LocalDataRepository dataRepository)
        {
            _navigation = navigation;
            _dataRepository = dataRepository;
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
                SaveCommand.ChangeCanExecute();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public DateTime CreateOn
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

        private async void Save()
        {
            var poi = new Data.Models.PointOfInterest
            {
                Latitude = Latitude,
                Longtitude = Longtitude,
                Name = Subject,
                Description = Description,
                Id = Guid.NewGuid(),
                Transmitted = false,
                CreateOn = DateTime.Now,
                User = _dataRepository.Configuration.User
            };
            _dataRepository.PointOfInterestList.Add(poi);

            await _dataRepository.Save();
            await _navigation.PopAsync();
        }

        private bool CanSave()
        {
            return !string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(Description);
        }
    }
}
