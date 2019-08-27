using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Services
{
    public class UserRoleRepository: RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        { }
    }
}
