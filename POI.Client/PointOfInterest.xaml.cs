using POI.Client.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace POI.Client
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PointOfInterest : ContentPage
	{
		public PointOfInterest(PointOfInterestViewModel viewModel)
		{
			InitializeComponent ();
		    this.BindingContext = viewModel;
		}
	    
	}
}