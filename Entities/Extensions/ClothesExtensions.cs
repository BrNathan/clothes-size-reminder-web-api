using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class ClothesExtensions
    {
        public static void ApplyChange(this Clothes dbClothes, Clothes clothes)
        {
            if (!String.IsNullOrWhiteSpace(clothes.Code))
            {
                dbClothes.Code = clothes.Code;
            }
            if (!String.IsNullOrWhiteSpace(clothes.Label))
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
