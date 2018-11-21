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

        bool AddUser(User user);
    }
}
