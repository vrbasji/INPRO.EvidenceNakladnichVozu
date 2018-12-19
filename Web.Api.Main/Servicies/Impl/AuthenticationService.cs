using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Api.Main.Servicies.Impl
{
    public class AuthenticationService : IAuth
    {
        IAuthenticationRepository _authRep;
        public AuthenticationService(IAuthenticationRepository authRep)
        {
            _authRep = authRep;
        }
        public string Authenticate(string username, string password)
        {
            return _authRep.Authenticate(username, password);
        }

        public bool IsAuthenticated(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            return _authRep.IsAuthenitcated(token);
        }
    }
}
