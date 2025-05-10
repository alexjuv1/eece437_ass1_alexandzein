using UniversityApp.Core.Entities;

namespace UniversityApp.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user, IList<string> roles);
    }
}
