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

        public int AddUser(User user)
        {
            try
            {
                var role = _dbContext.Roles.FirstOrDefault(x => x.RoleId == user.Role.RoleId);
                if (role == null) return -1;
                user.Role = role;
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return user.UserId;
            }
            catch (Exception)
            {
                return -1;
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
                var role = _dbContext.Roles.FirstOrDefault(x => x.RoleId == user.Role.RoleId);
                if (role == null) role = user.Role;
                ed.Role = role;
                _dbContext.SaveChanges();
            }
            return ed;
        }

        public List<Subject> GetAllSubjects(int start, int end)
        {
            return _dbContext.Subjects.Skip(start).Take(end).ToList();
        }

        public List<User> GetForPages(int start, int end)
        {
            return _dbContext.Users.Skip(start).Take(end).ToList();
        }

        public User GetUser(int userId)
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
