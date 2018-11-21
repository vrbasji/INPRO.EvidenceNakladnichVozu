using Data;
using Data.Repositories.Interfaces;
using Newtonsoft.Json;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Web.Api.Main.Servicies;

namespace Web.Api.Main.Controllers
{
    public class UserController : MyApiController
    {
        private IUserRepository _userRepository;

        public UserController(IAuth auth, IUserRepository userRepository) : base(auth)
        {
            _userRepository = userRepository;
        }

        [SwaggerOperation("GetAllUsers")]
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }
        [SwaggerOperation("GetUserInfo")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public User GetUserInfo(int id)
        {
            return _userRepository.GetUser(id);
        }

        [SwaggerOperation("GetLogedUserInfo")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public User GetLogedUserInfo()
        {
            var id = 5;//TODO: get logged user, nebo to spojit s jen GetUserInfo, at si to poresi na frontendu
            return _userRepository.GetUser(id);
        }
    }
}