using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using POI.Client.Data;
using POI.Client.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace POI.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PointOfInterestList : ContentPage
    {
        public PointOfInterestList(LocalDataRepository dataRepository)
        {
            InitializeComponent();
            BindingContext = new PointOfInterestListViewModel(dataRepository);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (this.BindingContext as PointOfInterestListViewModel)?.FillItems();
        }
    }
}
