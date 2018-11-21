using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Api.Main.Servicies.Impl
{
    public class AuthenticationService : IAuth
    {
        public string Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsAuthenticated(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            //TODO: pokoumat jestli je auth
            return true;
        }
    }
}
