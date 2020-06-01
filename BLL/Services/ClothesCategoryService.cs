using BLL.Interfaces;
using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System.Collections.Generic;

namespace BLL.Services
{
    public class ClothesCategoryService : IClothesCategoryService
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly IClothesCategoryRepository _clothesCategoryRepository;

        public ClothesCategoryService(RepositoryContext repositoryContext, IClothesCategoryRepository clothesCategoryRepository)
        {
            this._repositoryContext = repositoryContext;
            this._clothesCategoryRepository = clothesCategoryRepository;
        }

        public void Save()
        {
            this._repositoryContext.SaveChanges();
        }

        public void CreateClothesCategory(ClothesCategory clothesCategory)
        {
            _clothesCategoryRepository.Create(clothesCategory);
        }

        public void DeleteClothesCategory(ClothesCategory clothesCategory)
        {
            _clothesCategoryRepository.DeleteClothesCategory(clothesCategory);
        }

        public IEnumerable<ClothesCategory> GetAllClothesCategories()
        {
            return _clothesCategoryRepository.GetAllClothesCategories();
        }

        public ClothesCategory GetClothesCategoryById(int clothesCategoryId)
        {
            return _clothesCategoryRepository.GetClothesCategoryById(clothesCategoryId);
        }

        public void UpdateClothesCategory(ClothesCategory dbClothesCategory, ClothesCategory clothesCategory)
        {
            _clothesCategoryRepository.UpdateClothesCategory(dbClothesCategory, clothesCategory);
        }
    }
}
