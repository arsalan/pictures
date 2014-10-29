using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using StructureMap;
//using ImageOrganizer.Helpers;

namespace ImageOrganizer
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.DependencyResolver = new DependencyResolver(new Container(new ControllerRegistry()));
            //GlobalConfiguration.Configuration.Formatters.Insert(0, new EmberJsonMediaTypeFormatter());
        }
    }
}
