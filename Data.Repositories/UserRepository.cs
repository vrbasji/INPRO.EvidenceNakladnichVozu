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

        public User DeleteUser(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
            if (user == null) return null;
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
            return user;
        }

        public User EditUser(User user)
        {
            var ed = _dbContext.Users.FirstOrDefault(x => x.UserId == user.UserId);
            if(ed != null)
            {
                ed.Email = user.Email;
                ed.FirstName = user.FirstName;
                ed.LastName = user.LastName;
                ed.Role = user.Role;
                _dbContext.SaveChanges();
            }
            return ed;
        }

        public List<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public List<User> GetForPages(int start, int end)
        {
            return GetAll().Skip(start).Take(end).ToList();
        }

        public User GetUser(int userId)
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
