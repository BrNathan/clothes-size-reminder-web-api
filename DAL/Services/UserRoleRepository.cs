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
    public class UserRoleRepository: RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        { }

        public IEnumerable<UserRole> GetAllUserRoles()
        {
            return FindAll();
        }

        public UserRole GetUserRoleById(int id)
        {
            return FindByCondition(b => b.Id == id)
                .FirstOrDefault();
        }

        public void UpdateUserRole(UserRole dbUserRole, UserRole userRole)
        {
            dbUserRole.ApplyChange(userRole);
            Update(dbUserRole);
        }

        public void DeleteUserRole(UserRole userRole)
        {
            Delete(userRole);
        }
    }
}
