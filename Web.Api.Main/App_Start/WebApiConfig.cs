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
            container.RegisterType<ICarRepository, CarRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ISubjectRepository, SubjectRepository>();
            container.RegisterType<ISerieRepository, SerieRepository>();
            container.RegisterType<IBreakRepository, BreakRepository>();
            container.RegisterType<CarHistoryService>();
            container.RegisterType<ICarHistoryRepository, CarHistoryRepository>();
            // Controllers
            var auth = new ResolvedParameter<IAuth>();
            var userRepository = new ResolvedParameter<UserRepository>();
            var carRepository = new ResolvedParameter<CarRepository>();
            var serieRepository = new ResolvedParameter<SerieRepository>();
            var subjectRepository = new ResolvedParameter<SubjectRepository>();
            var breakRepository = new ResolvedParameter<BreakRepository>();
            container.RegisterType<MyApiController>(new InjectionConstructor(auth));
            container.RegisterType<UserController>(new InjectionConstructor(auth, userRepository));
            container.RegisterType<CarController>(new InjectionConstructor(auth, carRepository));
            container.RegisterType<SerieController>(new InjectionConstructor(auth, serieRepository));
            container.RegisterType<SubjectController>(new InjectionConstructor(auth, subjectRepository));
            container.RegisterType<BreakController>(new InjectionConstructor(auth, breakRepository));

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
