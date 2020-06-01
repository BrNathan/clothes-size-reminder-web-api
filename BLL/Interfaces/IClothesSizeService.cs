using Entities.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IClothesSizeService
    {
        IEnumerable<ClothesSize> GetAllClothesSizes();
        IEnumerable<ClothesSize> GetAllClothesSizesByIds(IEnumerable<int> clothesSizeIds);
        ClothesSize GetClothesSizeById(int clothesSizeId);
        void CreateClothesSize(ClothesSize clothesSize);
        void UpdateClothesSize(ClothesSize dbClothesSize, ClothesSize clothesSize);
        void DeleteClothesSize(ClothesSize clothesSize);
        void Save();
    }
}
