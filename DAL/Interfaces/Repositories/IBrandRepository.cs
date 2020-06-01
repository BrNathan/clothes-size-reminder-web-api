using Entities.Models;
using System.Collections.Generic;

namespace DAL.Interfaces.Repositories
{
    public interface IBrandRepository : IRepositoryBase<Brand>
    {
        IEnumerable<Brand> GetAllBrands();
        Brand GetBrandById(int id);
        void UpdateBrand(Brand dbBrand, Brand brand);
        void DeleteBrand(Brand brand);
    }
}
