using Autofac.Integration.WebApi;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BooksAPI.Models;
using BooksAPI.Repositories;

namespace BooksAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            // Create the Autofac container
            var builder = new ContainerBuilder();

            // Register your dependencies
            builder.RegisterType<BooksDBContext>().InstancePerRequest();
            builder.RegisterType<BookRepository>().As<IBookRepository>().InstancePerRequest();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerRequest();
            // Register your other services, repositories, etc.

            // Register API controllers
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

            // Build the container
            var container = builder.Build();

            // Set the WebApi dependency resolver to use Autofac
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
