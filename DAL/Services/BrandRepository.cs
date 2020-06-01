using DAL.Interfaces.Repositories;
using Entities;
using Entities.Extensions;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Services
{
    public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
    {
        public BrandRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return FindAll()
                .OrderBy(brand => brand.Name);
        }

        public Brand GetBrandById(int id)
        {
            return FindByCondition(b => b.Id == id)
                .FirstOrDefault();
        }

        public void UpdateBrand(Brand dbBrand, Brand brand)
        {
            dbBrand.ApplyChange(brand);
            Update(dbBrand);
        }

        public void DeleteBrand(Brand brand)
        {
            Delete(brand);
        }
    }
}
