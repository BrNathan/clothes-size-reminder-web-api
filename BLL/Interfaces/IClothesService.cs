using Entities.ExtendedModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IClothesService
    {
        IEnumerable<Clothes> GetAllClothes();
        Clothes GetClothesById(int clothesId);
        ClothesExtended GetClothesWithDetails(int clothesId);
        void CreateClothes(Clothes clothes);
        void UpdateClothes(Clothes dbClothes, Clothes clothes);
        void DeleteClothes(Clothes clothes);
        void Save();
    }
}
