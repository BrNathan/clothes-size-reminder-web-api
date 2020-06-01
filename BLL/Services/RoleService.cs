using BLL.Interfaces;
using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System.Collections.Generic;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly IRoleRepository _roleRepository;

        public RoleService(RepositoryContext repositoryContext, IRoleRepository roleRepository)
        {
            this._repositoryContext = repositoryContext;
            this._roleRepository = roleRepository;
        }

        public void Save()
        {
            this._repositoryContext.SaveChanges();
        }

        public void CreateRole(Role role)
        {
            _roleRepository.Create(role);
        }

        public void DeleteRole(Role role)
        {
            _roleRepository.DeleteRole(role);
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _roleRepository.GetAllRoles();
        }

        public Role GetRoleById(int roleId)
        {
            return _roleRepository.GetRoleById(roleId);
        }

        public void UpdateRole(Role dbRole, Role role)
        {
            _roleRepository.UpdateRole(dbRole, role);
        }
    }
}
