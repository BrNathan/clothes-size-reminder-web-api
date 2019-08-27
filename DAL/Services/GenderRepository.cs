using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Services
{
    public class GenderRepository: RepositoryBase<Gender>, IGenderRepository
    {
        public GenderRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        { }
    }
}
