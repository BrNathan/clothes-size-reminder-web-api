using DAL.Interfaces.Repositories;
using Entities;
using Entities.Extensions;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Services
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
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
