using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects.DataTransferObjects.UserDTOs;
using DataTransferObjects.SharedModels;
using Entities.DatabaseModels.UserModels;
using X.PagedList;

namespace Services.IServices
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
