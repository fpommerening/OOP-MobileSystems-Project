using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POI.Client.Data.FileSystem;
using POI.Client.ViewModels;
using Xamarin.Forms;

namespace POI.Client
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
		   

            InitializeComponent();

		    var pathService = DependencyService.Get<IPathService>();
		    this.BindingContext = new MainPageViewModel(Navigation, pathService);
        }
	    
	}
}
