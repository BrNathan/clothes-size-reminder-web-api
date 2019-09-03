﻿using Entities.ExtendedModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IClothesRepository: IRepositoryBase<Clothes>
    {
        IEnumerable<Clothes> GetAllClothes();

        Clothes GetClothesById(int clothesId);

        ClothesExtended GetClothesWithDetails(int clothesId);

        void CreateClothes(Clothes clothes);

        void UpdateClothes(Clothes dbClothes, Clothes clothes);

        void DeleteClothes(Clothes clothes);
    }
}
