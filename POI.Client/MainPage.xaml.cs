using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace POI.Client
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

	    private void MenuItem_OnClicked(object sender, EventArgs e)
	    {
	        var loc = new Location();
	        loc.NavigationProp = Navigation;
            Navigation.PushAsync(loc, true);
	    }
	}
}
