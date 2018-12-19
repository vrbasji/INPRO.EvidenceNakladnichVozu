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
        public IAuth _AuthenticationService;

        public MyApiController() { }

        public MyApiController(IAuth iauth)
        {
            _AuthenticationService = iauth;
        }
    }
}