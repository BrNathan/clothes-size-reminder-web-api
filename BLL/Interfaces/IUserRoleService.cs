using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IUserRoleService
    {
        IEnumerable<UserRole> GetAllUserRoles();
        UserRole GetUserRoleById(int userRoleId);
        void CreateUserRole(UserRole userRole);
        void UpdateUserRole(UserRole dbUserRole, UserRole userRole);
        void DeleteUserRole(UserRole userRole);
        void Save();
    }
}
