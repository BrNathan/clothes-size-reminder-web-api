﻿using Entities.Models;
using System;

namespace Entities.Extensions
{
    public static class ClothesCategoryExtensions
    {
        public static void ApplyChange(this ClothesCategory dbClothesCategory, ClothesCategory clothesCategory)
        {
            if (!String.IsNullOrWhiteSpace(clothesCategory.Code))
            {
                dbClothesCategory.Code = clothesCategory.Code;
            }
            if (!String.IsNullOrWhiteSpace(clothesCategory.Label))
            {
                dbClothesCategory.Label = clothesCategory.Label;
            }
        }
    }
}
