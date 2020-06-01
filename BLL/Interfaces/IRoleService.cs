using Entities.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRoles();
        Role GetRoleById(int roleId);
        void CreateRole(Role role);
        void UpdateRole(Role dbRole, Role role);
        void DeleteRole(Role role);
        void Save();
    }
}
