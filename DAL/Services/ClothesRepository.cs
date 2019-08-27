
using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Services
{
    public class ClothesRepository: RepositoryBase<Clothes>, IClothesRepository
    {
        public ClothesRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        { }
    }
}
