using Entities.Models;
using System.Collections.Generic;

namespace DAL.Interfaces.Repositories
{
    public interface IGenderRepository : IRepositoryBase<Gender>
    {
        IEnumerable<Gender> GetAllGenders();
        Gender GetGenderById(int id);
        void UpdateGender(Gender dbGender, Gender gender);
        void DeleteGender(Gender gender);
    }
}
