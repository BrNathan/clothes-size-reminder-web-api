using DAL.Interfaces.Repositories;
using Entities;
using Entities.Extensions;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Services
{
    public class ClothesCategoryRepository : RepositoryBase<ClothesCategory>, IClothesCategoryRepository
    {
        public ClothesCategoryRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public IEnumerable<ClothesCategory> GetAllClothesCategories()
        {
            return FindAll()
                .OrderBy(clothesCategory => clothesCategory.Label);
        }

        public ClothesCategory GetClothesCategoryById(int id)
        {
            return FindByCondition(b => b.Id == id)
                .FirstOrDefault();
        }

        public void UpdateClothesCategory(ClothesCategory dbClothesCategory, ClothesCategory clothesCategory)
        {
            dbClothesCategory.ApplyChange(clothesCategory);
            Update(dbClothesCategory);
        }

        public void DeleteClothesCategory(ClothesCategory clothesCategory)
        {
            Delete(clothesCategory);
        }
    }
}
