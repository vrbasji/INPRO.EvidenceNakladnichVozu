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
            //container.Register<DbContext>(new InjectionFactory(c => new ENVCtx()));
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

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}