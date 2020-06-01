using Entities.ExtendedModels;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        UserAuthenticate Authenticate(string email, string passwordHash);
    }
}
