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
            dbClothes.Code = clothes.Code;
            dbClothes.Label = clothes.Label;
            dbClothes.GenderId = clothes.GenderId;
            dbClothes.ClothesCategoryId = clothes.ClothesCategoryId;
        }
    }
}
