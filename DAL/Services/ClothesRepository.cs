
using DAL.Interfaces.Repositories;
using Entities;
using Entities.ExtendedModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Services
{
    public class ClothesRepository: RepositoryBase<Clothes>, IClothesRepository
    {
        public ClothesRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        { }

        public IEnumerable<Clothes> GetAllClothes()
        {
            return this.FindAll();
        }

        public Clothes GetClothesById(int clothesId)
        {
            return this.FindByCondition(clothes => clothes.Id.Equals(clothesId)).FirstOrDefault();
        }

        public ClothesExtended GetClothesWithDetails(int clothesId)
        {
            Clothes clothes = this.GetClothesById(clothesId);
            if (clothes == null)
            {
                return null;
            }
            return new ClothesExtended(clothes)
            {
                Gender = RepositoryContext.Genders.Where(g => g.Id == clothes.GenderId).FirstOrDefault(),
                ClothesCategory = RepositoryContext.ClothesCategories.Where(cc => cc.Id == clothes.ClothesCategoryId).FirstOrDefault()
            };
        }

        public void CreateClothes(Clothes clothes)
        {
            this.Create(clothes);
        }
    }
}
