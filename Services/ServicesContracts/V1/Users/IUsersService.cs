using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects.DTOs.Shared;
using DataTransferObjects.DTOs.Shared.Users;
using Entities.UserModels;
using X.PagedList;

namespace Services.ServicesContracts.V1.Users
{
    public interface IUsersService
    {
        Task<AccRole> CreateRole(AccRole role, CancellationToken cancellationToken);
        Task<ApplicationUser> CreateUser(ApplicationUser user, CancellationToken cancellationToken);

        Task<AccessToken> Login(string userName, string password, CancellationToken cancellationToken);

        Task<ApplicationUser> AddRoleToUser(long userId, long roleId, CancellationToken cancellationToken);
        Task<ApplicationUser> GetUserByUserName(ApplicationUser user, CancellationToken cancellationToken);

        Task<ApiResult<IPagedList<UserListDto>>> Search(UserSearchDto searchDto,
            CancellationToken cancellationToken);
    }
}
