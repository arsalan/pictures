using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ImageOrganizer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; 
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "PictureAlbum",
            //    routeTemplate: "api/{controller}/{pictureAlbumId}"
            //);

            //config.Routes.MapHttpRoute(
            //    name: "Pictures",
            //    routeTemplate: "api/pictureAlbums/{pictureAlbumId}/{controller}"
            //);

            //config.Routes.MapHttpRoute(
            //    name: "Picture",
            //    routeTemplate: "api/pictureAlbums/{pictureAlbumId}/{controller}/{pictureId}"
            //);

            config.Routes.MapHttpRoute(
                name: "PictureAlbum",
                routeTemplate: "api/{controller}/{pictureAlbumId}",
                defaults: new { pictureAlbumId = RouteParameter.Optional }
            );
        }
    }
}
