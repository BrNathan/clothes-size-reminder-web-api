using Entities.Models;
using System.Collections.Generic;

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
