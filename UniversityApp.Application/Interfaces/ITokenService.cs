using System.Threading.Tasks;

namespace UniversityApp.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(string userId);
    }
}
