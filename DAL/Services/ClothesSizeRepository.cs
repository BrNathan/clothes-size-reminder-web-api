using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Services
{
    public class ClothesSizeRepository: RepositoryBase<ClothesSize>, IClothesSizeRepository
    {
        public ClothesSizeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        { }

        public IEnumerable<ClothesSize> GetAllClothesSizes()
        {
            return FindAll();
        }

        public IEnumerable<ClothesSize> GetAllClothesSizesByIds(IEnumerable<int> clothesSizeIds)
        {
            return FindByCondition(cs => cs.Id.HasValue && clothesSizeIds.Contains(cs.Id.Value));
        }

        public ClothesSize GetClothesSizeById(int id)
        {
            return FindByCondition(b => b.Id == id)
                .FirstOrDefault();
        }

        public void UpdateClothesSize(ClothesSize dbClothesSize, ClothesSize clothesSize)
        {
            //dbClothesSize.ApplyChange(clothesSize);
            Update(dbClothesSize);
        }

        public void DeleteClothesSize(ClothesSize clothesSize)
        {
            Delete(clothesSize);
        }
    }
}
