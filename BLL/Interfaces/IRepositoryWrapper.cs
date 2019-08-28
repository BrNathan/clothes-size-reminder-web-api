using DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IRepositoryWrapper
    {
        IBrandRepository Brand { get; }
        IClothesCategoryRepository ClothesCategory { get; }
        IClothesRepository Clothes { get; }
        IClothesSizeRepository ClothesSize { get; }
        IGenderRepository Gender { get; }
        IReminderRepository Reminder { get; }
        IRoleRepository Role { get; }
        ISizeRepository Size { get; }
        IUserRepository User { get; }
        IUserRoleRepository UserRole { get; }
        void Save();
    }
}
