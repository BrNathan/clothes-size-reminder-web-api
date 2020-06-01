using DAL.Interfaces.Repositories;
using Entities;
using Entities.Extensions;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Services
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        { }

        public IEnumerable<Role> GetAllRoles()
        {
            return FindAll()
                .OrderBy(role => role.Label);
        }

        public Role GetRoleById(int id)
        {
            return FindByCondition(b => b.Id == id)
                .FirstOrDefault();
        }

        public void UpdateRole(Role dbRole, Role role)
        {
            dbRole.ApplyChange(role);
            Update(dbRole);
        }

        public void DeleteRole(Role role)
        {
            Delete(role);
        }
    }
}
