using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface ISizeRepository: IRepositoryBase<Size>
    {
        IEnumerable<Size> GetAllSizes();
        Size GetSizeById(int id);
        void UpdateSize(Size dbSize, Size size);
        void DeleteSize(Size size);
    }
}
