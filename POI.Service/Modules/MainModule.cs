using Nancy;

namespace POI.Service.Modules
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get("/", (args) => "Point of Interest API-Service");
        }

    }
}
