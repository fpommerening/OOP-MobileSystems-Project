using System;
using POI.Client.Data.FileSystem;

[assembly:Xamarin.Forms.Dependency(typeof(POI.Client.Droid.FileSystem.PathService))]
namespace POI.Client.Droid.FileSystem
{
    public class PathService : IPathService
    {
        public string GetDataPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        }
    }
}