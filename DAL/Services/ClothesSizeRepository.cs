using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Services
{
    public class ClothesSizeRepository: RepositoryBase<ClothesSize>, IClothesSizeRepository
    {
        public ClothesSizeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        { }
    }
}
