using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Main.Models;

namespace Web.Api.Main.Servicies.Impl
{
    public class AuthenticationService : IAuth
    {
        IAuthenticationRepository _authRep;
        public AuthenticationService(IAuthenticationRepository authRep)
        {
            _authRep = authRep;
        }
        public LoginResponseModel Authenticate(string username, string password)
        {
            var user = _authRep.Authenticate(username, password);
            if (user == null || user.Role == null) return null;
            return new LoginResponseModel() {
                RoleId = user.Role.RoleId,
                Token = user.Token,
                UserId = user.UserId
            };
        }

        public bool IsAuthenticated(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            return _authRep.IsAuthenitcated(token);
        }
    }
}
