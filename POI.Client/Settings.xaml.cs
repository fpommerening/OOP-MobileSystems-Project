using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POI.Client.Data;
using POI.Client.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace POI.Client
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
		public Settings (LocalDataRepository dataRepository, INavigation navigation)
		{
			InitializeComponent ();
            this.BindingContext = new SettingsViewModel(dataRepository, navigation);
		}
	}
}