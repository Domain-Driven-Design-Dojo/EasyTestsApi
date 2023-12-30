using Entities;
using System.Threading.Tasks;
using Entities.UserModels;
using DataTransferObjects.DTOs.Shared.Users;

namespace Services.ServicesContracts.BaseServices
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(ApplicationUser user);
    }
}