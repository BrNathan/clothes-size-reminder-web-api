using Entities.Models;
using System.Collections.Generic;

namespace DAL.Interfaces.Repositories
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        IEnumerable<Role> GetAllRoles();
        Role GetRoleById(int id);
        void UpdateRole(Role dbRole, Role role);
        void DeleteRole(Role role);
    }
}
