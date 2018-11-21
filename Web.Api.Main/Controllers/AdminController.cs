using Data;
using Data.Repositories.Interfaces;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Web.Api.Main.Servicies;

namespace Web.Api.Main.Controllers
{
    public class AdminController : MyApiController
    {
        private IUserRepository _userRepository;
        public AdminController(IAuth auth, IUserRepository userRepository) : base(auth)
        {
            _userRepository = userRepository;
        }

        [SwaggerOperation("AddUser")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public bool AddUser([FromBody]User user)
        {
            return _userRepository.AddUser(user);
        }
        [HttpPut]
        [SwaggerOperation("ChangeRole")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void ChangeRole(int id, [FromBody]Role role)
        {
            _userRepository.ChangeUserRole(id, role);
        }
    }
}