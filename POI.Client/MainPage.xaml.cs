using POI.Client.Data;
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
		    var localDataRepository = new LocalDataRepository(pathService.GetDataPath());
		    localDataRepository.Load().ConfigureAwait(false);
            this.BindingContext = new MainPageViewModel(Navigation, localDataRepository);
        }
	    
	}
}
