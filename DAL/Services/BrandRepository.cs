using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Services
{
    public class BrandRepository: RepositoryBase<Brand>, IBrandRepository
    {
        public BrandRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return FindAll()
                .OrderBy(brand => brand.Name)
                .ToList();
        }

        public Brand GetBrandById(int id)
        {
            return FindByCondition(b => b.Id == id)
                .FirstOrDefault();
        }
    }
}
