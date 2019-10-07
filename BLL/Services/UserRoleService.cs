using BLL.Interfaces;
using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(RepositoryContext repositoryContext, IUserRoleRepository userRoleRepository)
        {
            this._repositoryContext = repositoryContext;
            this._userRoleRepository = userRoleRepository;
        }

        public void Save()
        {
            this._repositoryContext.SaveChanges();
        }

        public void CreateUserRole(UserRole userRole)
        {
            _userRoleRepository.Create(userRole);
        }

        public void DeleteUserRole(UserRole userRole)
        {
            _userRoleRepository.DeleteUserRole(userRole);
        }

        public IEnumerable<UserRole> GetAllUserRoles()
        {
            return _userRoleRepository.GetAllUserRoles();
        }

        public UserRole GetUserRoleById(int userRoleId)
        {
            return _userRoleRepository.GetUserRoleById(userRoleId);
        }

        public void UpdateUserRole(UserRole dbUserRole, UserRole userRole)
        {
            _userRoleRepository.UpdateUserRole(dbUserRole, userRole);
        }
    }
}
