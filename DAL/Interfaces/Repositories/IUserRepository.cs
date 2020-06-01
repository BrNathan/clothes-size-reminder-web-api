using Entities.Models;
using System.Collections.Generic;

namespace DAL.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByEmailPaswword(string email, string passwordHash);
        void UpdateUser(User dbUser, User user);
        void DeleteUser(User user);
    }
}
