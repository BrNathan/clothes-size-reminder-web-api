using Entities.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IGenderService
    {
        IEnumerable<Gender> GetAllGenders();
        Gender GetGenderById(int genderId);
        void CreateGender(Gender gender);
        void UpdateGender(Gender dbGender, Gender gender);
        void DeleteGender(Gender gender);
        void Save();
    }
}
