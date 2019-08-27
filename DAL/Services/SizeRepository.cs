using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Services
{
    public class SizeRepository: RepositoryBase<Size>, ISizeRepository
    {
        public SizeRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        { }
    }
}
