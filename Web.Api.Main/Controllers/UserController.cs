using Data;
using Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Web.Http;
using Web.Api.Main.Models;
using Web.Api.Main.Servicies;

namespace Web.Api.Main.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : MyApiController, IDefaultMethods<User>
    {
        private IUserRepository _userRepository;

        public UserController(IAuth auth, IUserRepository userRepository) : base(auth)
        {
            _userRepository = userRepository;
        }
        [HttpPost]
        [Route("login")]
        public string Login(UserLogin data)
        {
           return _AuthenticationService.Authenticate(data.Email, data.Password);
        }

        [HttpPost]
        public int Add(User data)
        {
            return _userRepository.AddUser(data);
        }

        public void Edit(int id, User data)
        {
            data.UserId = id;
            _userRepository.EditUser(data);
        }

        public User Get(int id)
        {
            return _userRepository.GetUser(id);
        }

        public void Delete(int id)
        {
            _userRepository.DeleteUser(id);
        }

        public List<User> Get(int skip, int count)
        {
            return _userRepository.GetForPages(skip, count);
        }

        public List<User> Get(string query)
        {
            return _userRepository.FindUsers(query);
        }
    }
}