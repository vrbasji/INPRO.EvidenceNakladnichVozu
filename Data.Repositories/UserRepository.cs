using Data.Database;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ENVCtx _dbContext;

        public UserRepository(ENVCtx dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                return _dbContext.SaveChanges() == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangeUserRole(int userId, Role newRole)
        {
            try
            {
                var user = _dbContext.Users.First(x => x.UserId == userId);
                var role = _dbContext.Roles.First(x => x.RoleId == newRole.RoleId);
                user.Role = role;
                return _dbContext.SaveChanges() == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public User GetUser(int userId)
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
