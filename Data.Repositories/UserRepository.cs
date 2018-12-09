using Data.Database;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void DeleteUser(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
            if(user == null)
                throw new Exception("User with id " + userId + " was not found.");
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public void EditUser(User user)
        {
            var ed = _dbContext.Users.FirstOrDefault(x => x.UserId == user.UserId);
            if (ed == null)
                throw new Exception("User with id " + user.UserId + " was not found.");
            if (ed != null)
            {
                ed.Email = user.Email;
                ed.FirstName = user.FirstName;
                ed.LastName = user.LastName;
                var role = _dbContext.Roles.FirstOrDefault(x => x.RoleId == user.Role.RoleId);
                if (role == null) role = user.Role;
                ed.Role = role;
                _dbContext.SaveChanges();
            }
        }

        public List<User> FindUsers(string query)
        {
            return _dbContext.Users
                .Where(x => x.FirstName.Contains(query) || x.LastName.Contains(query) || x.Email.Contains(query))
                .ToList();
        }

        public List<User> GetForPages(int skip, int count)
        {
            return _dbContext.Users
                .OrderBy(x=>x.UserId)
                .Skip(skip)
                .Take(count)
                .ToList();
        }

        public User GetUser(int userId)
        {
            return _dbContext.Users
                .FirstOrDefault(x => x.UserId == userId);
        }
    }
}
