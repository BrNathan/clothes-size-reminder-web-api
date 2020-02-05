using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IClothesSizeRepository : IRepositoryBase<ClothesSize>
    {
        IEnumerable<ClothesSize> GetAllClothesSizes();
        IEnumerable<ClothesSize> GetAllClothesSizesByIds(IEnumerable<int> clothesSizeIds);
        ClothesSize GetClothesSizeById(int id);
        ClothesSize GetClothesSizeBySizeIdAndClothesId(int sizeId, int clothesId);
        void UpdateClothesSize(ClothesSize dbClothesSize, ClothesSize clothesSize);
        void DeleteClothesSize(ClothesSize clothesSize);
    }
}
