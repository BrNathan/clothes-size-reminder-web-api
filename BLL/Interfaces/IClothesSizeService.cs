using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IClothesSizeService
    {
        IEnumerable<ClothesSize> GetAllClothesSizes();
        ClothesSize GetClothesSizeById(int clothesSizeId);
        void CreateClothesSize(ClothesSize clothesSize);
        void UpdateClothesSize(ClothesSize dbClothesSize, ClothesSize clothesSize);
        void DeleteClothesSize(ClothesSize clothesSize);
        void Save();
    }
}
