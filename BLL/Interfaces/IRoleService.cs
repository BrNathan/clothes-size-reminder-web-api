using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
