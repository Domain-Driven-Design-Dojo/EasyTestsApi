using Entities;
using System.Threading.Tasks;
using Entities.DatabaseModels.UserModels;

namespace Services
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(ApplicationUser user);
    }
}