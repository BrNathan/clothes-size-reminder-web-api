using Entities.ExtendedModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        UserAuthenticate Authenticate(string email, string passwordHash);
    }
}
