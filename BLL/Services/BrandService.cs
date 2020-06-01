using BLL.Interfaces;
using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System.Collections.Generic;

namespace BLL.Services
{
    public class BrandService : IBrandService
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly IBrandRepository _brandRepository;

        public BrandService(RepositoryContext repositoryContext, IBrandRepository brandRepository)
        {
            this._repositoryContext = repositoryContext;
            this._brandRepository = brandRepository;
        }

        public void Save()
        {
            this._repositoryContext.SaveChanges();
        }

        public void CreateBrand(Brand brand)
        {
            _brandRepository.Create(brand);
        }

        public void DeleteBrand(Brand brand)
        {
            _brandRepository.DeleteBrand(brand);
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return _brandRepository.GetAllBrands();
        }

        public Brand GetBrandById(int brandId)
        {
            return _brandRepository.GetBrandById(brandId);
        }

        public void UpdateBrand(Brand dbBrand, Brand brand)
        {
            _brandRepository.UpdateBrand(dbBrand, brand);
        }
    }
}
