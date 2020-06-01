using Entities.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IBrandService
    {
        IEnumerable<Brand> GetAllBrands();
        Brand GetBrandById(int brandId);
        void CreateBrand(Brand brand);
        void UpdateBrand(Brand dbBrand, Brand brand);
        void DeleteBrand(Brand brand);
        void Save();
    }
}
