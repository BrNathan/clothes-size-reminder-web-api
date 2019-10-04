using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IClothesSizeRepository : IRepositoryBase<ClothesSize>
    {
        IEnumerable<ClothesSize> GetAllClothesSizes();
        ClothesSize GetClothesSizeById(int id);
        void UpdateClothesSize(ClothesSize dbClothesSize, ClothesSize clothesSize);
        void DeleteClothesSize(ClothesSize clothesSize);
    }
}
