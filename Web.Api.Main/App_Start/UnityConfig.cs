using Data.Database;
using Data.Repositories;
using Data.Repositories.Interfaces;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.WebApi;
using Web.Api.Main.Controllers;
using Web.Api.Main.Servicies;
using Web.Api.Main.Servicies.Impl;

namespace Web.Api.Main
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
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
            container.RegisterType<HandBreakController>(new InjectionConstructor(auth, breakRepository));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}