using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Web.Api.Main.Services;

[assembly: OwinStartup(typeof(Web.Api.Main.App_Start.Startup))]

namespace Web.Api.Main.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureOAuth(app);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
