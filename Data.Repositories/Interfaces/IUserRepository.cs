using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();

        User GetUser(int userId);

        bool ChangeUserRole(int userId, Role newRole);

        int AddUser(User user);

        User EditUser(User user);

        User DeleteUser(int userId);

        List<User> GetForPages(int start, int end);
    }
}
