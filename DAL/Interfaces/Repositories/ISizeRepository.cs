using Entities.Models;
using System.Collections.Generic;

namespace DAL.Interfaces.Repositories
{
    public interface ISizeRepository : IRepositoryBase<Size>
    {
        IEnumerable<Size> GetAllSizes();
        Size GetSizeById(int id);
        void UpdateSize(Size dbSize, Size size);
        void DeleteSize(Size size);
    }
}
