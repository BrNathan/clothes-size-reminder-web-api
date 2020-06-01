using Entities.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IClothesCategoryService
    {
        IEnumerable<ClothesCategory> GetAllClothesCategories();
        ClothesCategory GetClothesCategoryById(int clothesCategoryId);
        void CreateClothesCategory(ClothesCategory clothesCategory);
        void UpdateClothesCategory(ClothesCategory dbClothesCategory, ClothesCategory clothesCategory);
        void DeleteClothesCategory(ClothesCategory clothesCategory);
        void Save();
    }
}
