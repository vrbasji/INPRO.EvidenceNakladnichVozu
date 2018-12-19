using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http.Formatting;
using System.Net.Http;
using Unity.Injection;
using Data.Repositories;
using Data.Repositories.Interfaces;
using System.Web.Http.Routing;

namespace Web.Api.Main.Servicies
{
    public class MyFilter : ActionFilterAttribute
    {
        private IAuth _authService;
        public MyFilter(IAuth authService)
        {
            _authService = authService;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var routeTemplate = actionContext.Request.RequestUri.AbsoluteUri;
            if (routeTemplate.Contains("api/user/login"))
            {
                base.OnActionExecuting(actionContext);
                return;
            }
            bool authorized = false;
            IEnumerable<string> values;
            actionContext.Request.Headers.TryGetValues("Token", out values);
            string token = values.FirstOrDefault();

            authorized = _authService.IsAuthenticated(token);

            if (!authorized)
            {
                var response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Headers.Location = new Uri("https://thomaash.github.io/inpro");
                actionContext.Response = response;
                base.OnActionExecuting(actionContext);
                return;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}