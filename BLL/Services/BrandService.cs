using BLL.Interfaces;
using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class BrandService : IBrandService
    {
        private RepositoryContext _repositoryContext;
        private IBrandRepository _brandRepository;

        public BrandService(RepositoryContext repositoryContext)
        {
            this._repositoryContext = repositoryContext;

        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        void IBrandService.CreateBrand(Brand brand)
        {
            throw new NotImplementedException();
        }

        void IBrandService.DeleteBrand(Brand brand)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Brand> IBrandService.GetAllBrands()
        {
            throw new NotImplementedException();
        }

        Brand IBrandService.GetBrandById(int brandId)
        {
            throw new NotImplementedException();
        }

        void IBrandService.UpdateBrand(Brand dbBrand, Brand brand)
        {
            throw new NotImplementedException();
        }
    }
}
