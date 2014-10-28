using System.Web.Http.Controllers;
using StructureMap.Configuration.DSL;
using ImageOrganizer.DataStore;

namespace ImageOrganizer
{
    public class ControllerRegistry : Registry
    {
        public ControllerRegistry()
        {
            For<IPictureAlbumsRepository>().Use<Version1InMemoryPictureAlbumRepository>();
        }
    }
}