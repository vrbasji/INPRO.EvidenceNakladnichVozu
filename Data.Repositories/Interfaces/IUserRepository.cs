using System.Collections.Generic;

namespace Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int userId);

        int AddUser(User user);

        void EditUser(User user);

        void DeleteUser(int userId);

        List<User> GetForPages(int skip, int count);

        List<User> FindUsers(string query);
    }
}
