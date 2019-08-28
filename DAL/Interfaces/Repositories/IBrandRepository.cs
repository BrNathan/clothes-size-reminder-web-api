using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IBrandRepository: IRepositoryBase<Brand>
    {
        IEnumerable<Brand> GetAllBrands();
    }
}
