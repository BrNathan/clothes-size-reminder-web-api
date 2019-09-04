using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class ClothesExtensions
    {
        public static void Map(this Clothes dbClothes, Clothes clothes)
        {
            if (clothes.Code != null)
            {
                dbClothes.Code = clothes.Code;
            }
            if (clothes.Label != null)
            {
                dbClothes.Label = clothes.Label;
            }
            if (clothes.GenderId.HasValue)
            {
                dbClothes.GenderId = clothes.GenderId;
            }
            if (clothes.ClothesCategoryId != default(int))
            {
                dbClothes.ClothesCategoryId = clothes.ClothesCategoryId;
            }
        }
    }
}
