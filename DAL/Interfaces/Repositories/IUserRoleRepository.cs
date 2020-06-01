using Entities.Models;
using System.Collections.Generic;

namespace DAL.Interfaces.Repositories
{
    public interface IUserRoleRepository : IRepositoryBase<UserRole>
    {
        IEnumerable<UserRole> GetAllUserRoles();
        UserRole GetUserRoleById(int id);
        void UpdateUserRole(UserRole dbUserRole, UserRole userRole);
        void DeleteUserRole(UserRole userRole);
    }
}
