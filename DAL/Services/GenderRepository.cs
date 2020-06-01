using DAL.Interfaces.Repositories;
using Entities;
using Entities.Extensions;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Services
{
    public class GenderRepository : RepositoryBase<Gender>, IGenderRepository
    {
        public GenderRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        { }

        public IEnumerable<Gender> GetAllGenders()
        {
            return FindAll()
                .OrderBy(gender => gender.Label);
        }

        public Gender GetGenderById(int id)
        {
            return FindByCondition(b => b.Id == id)
                .FirstOrDefault();
        }

        public void UpdateGender(Gender dbGender, Gender gender)
        {
            dbGender.ApplyChange(gender);
            Update(dbGender);
        }

        public void DeleteGender(Gender gender)
        {
            Delete(gender);
        }
    }
}
