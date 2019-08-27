using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Services
{
    public class ClothesCategoryRepository : RepositoryBase<ClothesCategory>, IClothesCategoryRepository
    {
        public ClothesCategoryRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }
    }
}
