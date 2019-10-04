using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IClothesCategoryRepository : IRepositoryBase<ClothesCategory>
    {
        IEnumerable<ClothesCategory> GetAllClothesCategories();
        ClothesCategory GetClothesCategoryById(int id);
        void UpdateClothesCategory(ClothesCategory dbClothesCategory, ClothesCategory clothesCategory);
        void DeleteClothesCategory(ClothesCategory clothesCategory);
    }
}
