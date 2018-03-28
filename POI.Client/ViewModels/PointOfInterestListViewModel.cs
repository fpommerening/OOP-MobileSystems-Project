using System;
using System.Collections.ObjectModel;
using System.Linq;
using POI.Client.Data;
using Xamarin.Forms;

namespace POI.Client.ViewModels
{
    public class PointOfInterestListViewModel : BaseViewModel
    {
        private readonly LocalDataRepository _dataRepository;

        public PointOfInterestListViewModel(LocalDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            TransmitCommand = new Command(Transmit, CanTransmit);
            ClearCommand = new Command(Clear, CanClear);
            Items = new ObservableCollection<PointOfInterestListItemViewModel>();
        }

        public ObservableCollection<PointOfInterestListItemViewModel> Items { get; set; }

        public void FillItems()
        {
            Items.Clear();
            foreach (var dbo in _dataRepository.PointOfInterestList)
            {
                Items.Add(new PointOfInterestListItemViewModel
                {
                    Latitude = dbo.Latitude,
                    Longtitude = dbo.Longtitude,
                    Subject = dbo.Name,
                    CreateOn = dbo.CreateOn,
                    Transmitted = dbo.Transmitted,
                    Description =dbo.Description
                });

            }
            TransmitCommand.ChangeCanExecute();
            ClearCommand.ChangeCanExecute();
        }

        public Command TransmitCommand { get; set; }

        public Command ClearCommand { get; set; }

        public async void Transmit()
        {
            if (string.IsNullOrEmpty(_dataRepository.Configuration.ServiceUrl))
            {
                return;
            }

            var sc = new ServiceClient(_dataRepository.Configuration.ServiceUrl);
            var poiToTransmit = _dataRepository.PointOfInterestList.Where(x => !x.Transmitted);
            foreach (var poi in poiToTransmit)
            {
                var dto = new Contracts.PointOfInterest
                {
                    Latitude = (int) Math.Ceiling(poi.Latitude * 100000),
                    Longtitude = (int) Math.Ceiling(poi.Longtitude * 100000),
                    Description = poi.Description,
                    Name = poi.Name,
                    User = poi.User,
                    CreateOn = poi.CreateOn,
                    Id = poi.Id
                };
                await sc.SavePointOfInterest(dto);
                poi.Transmitted = true;
            }
            await _dataRepository.Save();
            FillItems();
        }

        public bool CanTransmit()
        {
            return Items.Any(x => !x.Transmitted);
        }

        public async void Clear()
        {
            _dataRepository.PointOfInterestList.Clear();
            await _dataRepository.Save();
            FillItems();
        }

        public bool CanClear()
        {
            return Items.Any();
        }
    }
}
