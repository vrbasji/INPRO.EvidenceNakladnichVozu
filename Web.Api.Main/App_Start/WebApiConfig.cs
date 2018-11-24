using Data.Database;
using Data.Repositories;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;
using Unity.Injection;
using Unity.WebApi;
using Web.Api.Main.Controllers;
using Web.Api.Main.Servicies;
using Web.Api.Main.Servicies.Impl;

namespace Web.Api.Main
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // povolit cors
            var cors = new EnableCorsAttribute("https://thomaash.github.io", "*", "*");
            config.EnableCors(cors);

            var container = new UnityContainer();
            // Database
            container.RegisterType<DbContext, ENVCtx>();
            // AuthService
            container.RegisterType<IAuth, AuthenticationService>();
            // Repositories
            var context = new ResolvedParameter<ENVCtx>();
            container.RegisterType<ICarRepository, CarRepository>(/*(new InjectionConstructor(context))*/);
            container.RegisterType<IUserRepository, UserRepository>(/*(new InjectionConstructor(context))*/);
            // Controllers
            var auth = new ResolvedParameter<IAuth>();
            var userRepository = new ResolvedParameter<UserRepository>();
            container.RegisterType<MyApiController>(new InjectionConstructor(auth));
            container.RegisterType<UserController>(new InjectionConstructor(auth, userRepository));

            config.DependencyResolver = new UnityDependencyResolver(container);

            // Pro navrat json
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            // Aby json nezasekl v loopu
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
