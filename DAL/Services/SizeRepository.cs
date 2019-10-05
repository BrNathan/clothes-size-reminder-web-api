using DAL.Interfaces.Repositories;
using Entities;
using Entities.Extensions;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Services
{
    public class SizeRepository: RepositoryBase<Size>, ISizeRepository
    {
        public SizeRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        { }

        public IEnumerable<Size> GetAllSizes()
        {
            return FindAll()
                .OrderBy(size => size.Label);
        }

        public Size GetSizeById(int id)
        {
            return FindByCondition(b => b.Id == id)
                .FirstOrDefault();
        }

        public void UpdateSize(Size dbSize, Size size)
        {
            dbSize.ApplyChange(size);
            Update(dbSize);
        }

        public void DeleteSize(Size size)
        {
            Delete(size);
        }
    }
}
