using Data;
using Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Web.Http;
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
        [Route]
        public int Add(User data)
        {
            return _userRepository.AddUser(data);
        }
        [Route]
        [HttpPut]
        public void Edit(User data)
        {
            _userRepository.EditUser(data);
        }

        [Route("{id}")]
        public User Get(int id)
        {
            return _userRepository.GetUser(id);
        }

        [Route("{id}")]
        public void Delete(int id)
        {
            _userRepository.DeleteUser(id);
        }

        [Route("{skip}/{count}")]
        public List<User> Get(int skip, int count)
        {
            return _userRepository.GetForPages(skip, count);
        }

        [Route("{query}")]
        public List<User> Get(string query)
        {
            return _userRepository.FindUsers(query);
        }
    }
}