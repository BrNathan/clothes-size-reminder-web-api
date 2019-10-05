using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ISizeService
    {
        IEnumerable<Size> GetAllSizes();
        Size GetSizeById(int sizeId);
        void CreateSize(Size size);
        void UpdateSize(Size dbSize, Size size);
        void DeleteSize(Size size);
        void Save();
    }
}
