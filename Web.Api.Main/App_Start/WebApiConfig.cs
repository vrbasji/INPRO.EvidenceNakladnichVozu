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
            container.RegisterType<IRevisionRepository, RevisionRepository>();
            container.RegisterType<IRepairRepository, RepairRepository>();
            container.RegisterType<IRentRepository, RentRepository>();
            container.RegisterType<IFaultRepository, FaultRepository>();
            container.RegisterType<IGoodGroupRepository, GoodGroupRepository>();
            // Controllers
            var auth = new ResolvedParameter<IAuth>();
            var userRepository = new ResolvedParameter<UserRepository>();
            var carRepository = new ResolvedParameter<CarRepository>();
            var serieRepository = new ResolvedParameter<SerieRepository>();
            var subjectRepository = new ResolvedParameter<SubjectRepository>();
            var breakRepository = new ResolvedParameter<BreakRepository>();
            var carHistoryRepository = new ResolvedParameter<CarHistoryRepository>();
            var revisionRepository = new ResolvedParameter<RevisionRepository>();
            var repairRepository = new ResolvedParameter<RepairRepository>();
            var rentRepository = new ResolvedParameter<RentRepository>();
            var faultRepository = new ResolvedParameter<FaultRepository>();
            var goodGroupRepository = new ResolvedParameter<GoodGroupRepository>();
            container.RegisterType<MyApiController>(new InjectionConstructor(auth));
            container.RegisterType<UserController>(new InjectionConstructor(auth, userRepository));
            container.RegisterType<CarController>(new InjectionConstructor(auth, carRepository, carHistoryRepository));
            container.RegisterType<SerieController>(new InjectionConstructor(auth, serieRepository));
            container.RegisterType<SubjectController>(new InjectionConstructor(auth, subjectRepository));
            container.RegisterType<HandBreakController>(new InjectionConstructor(auth, breakRepository));
            container.RegisterType<AirBreakController>(new InjectionConstructor(auth, breakRepository));
            container.RegisterType<FaultController>(new InjectionConstructor(auth, faultRepository));
            container.RegisterType<RentController>(new InjectionConstructor(auth, rentRepository));
            container.RegisterType<RepairController>(new InjectionConstructor(auth, repairRepository));
            container.RegisterType<RevisionController>(new InjectionConstructor(auth, revisionRepository));
            container.RegisterType<GoodGroupController>(new InjectionConstructor(auth, goodGroupRepository));

            config.DependencyResolver = new UnityDependencyResolver(container);

            // Pro navrat json
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            // Aby json nezasekl v loopu
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "RentsToApi",
                routeTemplate: "api/rent/rentto"
            );
            config.Routes.MapHttpRoute(
                name: "RentsFromApi",
                routeTemplate: "api/rent/rentfrom"
            );

            config.Routes.MapHttpRoute(
                name: "FaultsApi",
                routeTemplate: "api/{controller}/carfaults/{id}"
            );

            config.Routes.MapHttpRoute(
                name: "QueryApi",
                routeTemplate: "api/{controller}/find/{query}"
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "ListApi",
                routeTemplate: "api/{controller}/{skip}/{count}"
                );
        }
    }
}
