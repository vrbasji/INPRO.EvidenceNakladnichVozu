using Data.Database;
using Data.Repositories.Helpers;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private ENVCtx _dbContext;

        public AuthenticationRepository(ENVCtx dbContext)
        {
            _dbContext = dbContext;
        }
        public User Authenticate(string username, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == username && x.Password == password);
            if (user == null) return null;
            if(user.TokenValidTo < DateTime.Now)
            {
                return user;
            }
            var token = TokenManager.CreateToken();
            user.Token = token;
            user.TokenValidTo = DateTime.Now.AddHours(10);
            _dbContext.SaveChanges();
            return user;
        }

        public bool IsAuthenitcated(string token)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Token == token) != null;
        }
    }
}
