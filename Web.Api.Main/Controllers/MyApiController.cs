using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Web.Api.Main.Servicies;

namespace Web.Api.Main.Controllers
{
    public class MyApiController : ApiController
    {
        private IAuth _AuthenticationService;

        public MyApiController() { }

        public MyApiController(IAuth iauth)
        {
            _AuthenticationService = iauth;
        }

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    //var cookies = filterContext.HttpContext.Request.Cookies;
        //    //if(cookies["token"] == null)
        //    //{
        //    //    throw new HttpResponseException(HttpStatusCode.Unauthorized);
        //    //}
        //    //var token = cookies["token"].Value;
        //    //if (!_AuthenticationService.IsAuthenticated(token))
        //    //{
        //    //    throw new HttpResponseException(HttpStatusCode.Unauthorized);
        //    //}

        //    base.OnActionExecuting(filterContext);
        //}
    }
}