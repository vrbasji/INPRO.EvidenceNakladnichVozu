using Data;
using Data.Repositories.Interfaces;
using Newtonsoft.Json;
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
    public class UserController : MyApiController
    {
        private IUserRepository _userRepository;

        public UserController(IAuth auth, IUserRepository userRepository) : base(auth)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        [Route("api/user")]
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }
        [HttpGet]
        [Route("api/user/{startId}/{endId}")]
        public IEnumerable<User> GetAllPages(int startId, int endId)
        {
            return _userRepository.GetForPages(startId,endId);
        }
        [HttpGet]
        [Route("api/user/{id}")]
        public User GetUserInfo(int id)
        {
            return _userRepository.GetUser(id);
        }

        [HttpPost]
        [Route("api/user")]
        public int AddUser([FromBody]User user)
        {
            return _userRepository.AddUser(user);
        }

        [HttpPut]
        [Route("api/user")]
        public User EditUser([FromBody]User user)
        {
            return _userRepository.EditUser(user);
        }
        [HttpDelete]
        [Route("api/user/{id}")]
        public User DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }
    }
}