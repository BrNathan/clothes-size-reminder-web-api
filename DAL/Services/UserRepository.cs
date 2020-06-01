using DAL.Interfaces.Repositories;
using Entities;
using Entities.Extensions;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Services
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        { }

        public IEnumerable<User> GetAllUsers()
        {
            return FindAll()
                .OrderBy(user => user.FirstName);
        }

        public User GetUserById(int id)
        {
            return FindByCondition(b => b.Id == id)
                .FirstOrDefault();
        }

        public User GetUserByEmailPaswword(string email, string passwordHash)
        {
            return FindByCondition(user => user.Email == email && user.Password == passwordHash)
                .FirstOrDefault();
        }


        public void UpdateUser(User dbUser, User user)
        {
            dbUser.ApplyChange(user);
            Update(dbUser);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }
    }
}
