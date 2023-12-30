using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects.DTOs.BaseDtos;
using DataTransferObjects.DTOs.Shared;
using DataTransferObjects.DTOs.Shared.Users;
using X.PagedList;

namespace Services.ServicesContracts.V2.Users
{
    public interface IUsersService
    {
        //Task<List<ApplicationUser>> GetAllUsers(CancellationToken cancellationToken);

        Task<ApiResult<IPagedList<ApplicationUserListDto>>> GetUsersByRoleId(ApplicationUserSearchDto dto, CancellationToken cancellationToken);
        //Task<AccRole> CreateRole(AccRole role, CancellationToken cancellationToken);
        Task<ApiResult<ApplicationUserListDto>> CreateUser(ApplicationUserCuDto user, CancellationToken cancellationToken);
        Task<ApplicationUserListDto> GetUserById(long userId, CancellationToken cancellationToken);
        Task<AccessToken> Login(string userName, string password, CancellationToken cancellationToken);
        Task<ApiResult<ApplicationUserListDto>> AddRoleToUser(long userId, long roleId, CancellationToken cancellationToken);
        Task<ApiResult<List<ApplicationUserListDto>>> GetUsersByNationalId(ApplicationUserSearchDto user, CancellationToken cancellationToken);

        Task<ApiResult<IPagedList<ApplicationUserListDto>>> Get(ApplicationUserSearchDto searchDto,
            CancellationToken cancellationToken);

        Task<ApiResult<ApplicationUserListDto>> ChangePassword(
            ApplicationUserSearchDto.ApplicationUserChangePasswordDto dto, bool isAdmin,
            CancellationToken cancellationToken);

        Task<ApplicationUserListDto> UpdateUser(ApplicationUserCuDto user, CancellationToken cancellationToken);

        Task<ApiResult<ApplicationUserListDto>> RemoveRoleFromUser(long userId, long roleId, CancellationToken cancellationToken);

        Task<ApiResult<IPagedList<ApplicationUserListDto>>> GetUsersByGroupId(ApplicationUserSearchDto dto, CancellationToken cancellationToken);

        Task<ApiResult<BaseBoolDto>> UserIsInRole(long userId, string roleName, CancellationToken cancellationToken);
        Task<ApiResult<BaseBoolDto>> UserIsInRoleByRoleId(long userId, long roleId, CancellationToken cancellationToken);

    }
}
