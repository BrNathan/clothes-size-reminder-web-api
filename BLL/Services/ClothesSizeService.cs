using BLL.Interfaces;
using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class ClothesSizeService : IClothesSizeService
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly IClothesSizeRepository _clothesSizeRepository;

        public ClothesSizeService(RepositoryContext repositoryContext, IClothesSizeRepository clothesSizeRepository)
        {
            this._repositoryContext = repositoryContext;
            this._clothesSizeRepository = clothesSizeRepository;
        }

        public void Save()
        {
            this._repositoryContext.SaveChanges();
        }

        public void CreateClothesSize(ClothesSize clothesSize)
        {
            var existingClothesSize = _clothesSizeRepository.GetClothesSizeBySizeIdAndClothesId(clothesSize.SizeId, clothesSize.ClothesId);
            if (existingClothesSize == null)
            {
                _clothesSizeRepository.Create(clothesSize);
            }
            else
            {
                clothesSize.Id = existingClothesSize.Id;
            }
        }

        public void DeleteClothesSize(ClothesSize clothesSize)
        {
            _clothesSizeRepository.DeleteClothesSize(clothesSize);
        }

        public IEnumerable<ClothesSize> GetAllClothesSizes()
        {
            return _clothesSizeRepository.GetAllClothesSizes();
        }

        public IEnumerable<ClothesSize> GetAllClothesSizesByIds(IEnumerable<int> clothesSizeIds)
        {
            return _clothesSizeRepository.GetAllClothesSizesByIds(clothesSizeIds);
        }

        public ClothesSize GetClothesSizeById(int clothesSizeId)
        {
            return _clothesSizeRepository.GetClothesSizeById(clothesSizeId);
        }

        public void UpdateClothesSize(ClothesSize dbClothesSize, ClothesSize clothesSize)
        {
            _clothesSizeRepository.UpdateClothesSize(dbClothesSize, clothesSize);
        }
    }
}
