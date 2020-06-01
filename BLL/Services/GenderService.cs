using BLL.Interfaces;
using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System.Collections.Generic;

namespace BLL.Services
{
    public class GenderService : IGenderService
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly IGenderRepository _genderRepository;

        public GenderService(RepositoryContext repositoryContext, IGenderRepository genderRepository)
        {
            this._repositoryContext = repositoryContext;
            this._genderRepository = genderRepository;
        }

        public void Save()
        {
            this._repositoryContext.SaveChanges();
        }

        public void CreateGender(Gender gender)
        {
            _genderRepository.Create(gender);
        }

        public void DeleteGender(Gender gender)
        {
            _genderRepository.DeleteGender(gender);
        }

        public IEnumerable<Gender> GetAllGenders()
        {
            return _genderRepository.GetAllGenders();
        }

        public Gender GetGenderById(int genderId)
        {
            return _genderRepository.GetGenderById(genderId);
        }

        public void UpdateGender(Gender dbGender, Gender gender)
        {
            _genderRepository.UpdateGender(dbGender, gender);
        }
    }
}
