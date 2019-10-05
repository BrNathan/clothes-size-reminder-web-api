using BLL.Interfaces;
using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class SizeService: ISizeService
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly ISizeRepository _sizeRepository;

        public SizeService(RepositoryContext repositoryContext, ISizeRepository sizeRepository)
        {
            this._repositoryContext = repositoryContext;
            this._sizeRepository = sizeRepository;
        }

        public void Save()
        {
            this._repositoryContext.SaveChanges();
        }

        public void CreateSize(Size size)
        {
            _sizeRepository.Create(size);
        }

        public void DeleteSize(Size size)
        {
            _sizeRepository.DeleteSize(size);
        }

        public IEnumerable<Size> GetAllSizes()
        {
            return _sizeRepository.GetAllSizes();
        }

        public Size GetSizeById(int sizeId)
        {
            return _sizeRepository.GetSizeById(sizeId);
        }

        public void UpdateSize(Size dbSize, Size size)
        {
            _sizeRepository.UpdateSize(dbSize, size);
        }
    }
}
