using Entities.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int userId);
        User GetUserByEmailPaswword(string email, string passwordHash);
        void CreateUser(User user);
        void UpdateUser(User dbUser, User user);
        void DeleteUser(User user);
        void Save();
    }
}
