using BLL.Interfaces;
using DAL.Interfaces.Repositories;
using Entities;
using Entities.ExtendedModels;
using Entities.Models;
using System.Collections.Generic;

namespace BLL.Services
{
    public class ClothesService : IClothesService
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly IClothesRepository _clothesRepository;

        public ClothesService(RepositoryContext repositoryContext, IClothesRepository clothesRepository)
        {
            this._repositoryContext = repositoryContext;
            this._clothesRepository = clothesRepository;
        }

        public void Save()
        {
            this._repositoryContext.SaveChanges();
        }

        public void CreateClothes(Clothes clothes)
        {
            _clothesRepository.Create(clothes);
        }

        public void DeleteClothes(Clothes clothes)
        {
            _clothesRepository.DeleteClothes(clothes);
        }

        public IEnumerable<Clothes> GetAllClothes()
        {
            return _clothesRepository.GetAllClothes();
        }

        public Clothes GetClothesById(int clothesId)
        {
            return _clothesRepository.GetClothesById(clothesId);
        }

        public ClothesExtended GetClothesWithDetails(int clothesId)
        {
            return _clothesRepository.GetClothesWithDetails(clothesId);
        }

        public void UpdateClothes(Clothes dbClothes, Clothes clothes)
        {
            _clothesRepository.UpdateClothes(dbClothes, clothes);
        }
    }
}
