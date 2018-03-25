using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace POI.Client
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Location : ContentPage
	{
		public Location ()
		{
			InitializeComponent ();
		}

        public INavigation NavigationProp { get; set; }

	    private void Button_OnClicked(object sender, EventArgs e)
	    {
	        NavigationProp.PopToRootAsync(true);
	    }
	}
}