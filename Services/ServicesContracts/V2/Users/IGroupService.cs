using DataTransferObjects.DTOs.Shared;
using DataTransferObjects.DTOs.Shared.Users;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace Services.ServicesContracts.V2.Users
{
    public interface IGroupService
    {
        Task<ApiResult<IPagedList<GroupListDto>>> GetGroupsByRoleId(GroupSearchDto dto, CancellationToken cancellationToken);
        Task<ApiResult<IPagedList<GroupListDto>>> GetGroupsByUserId(GroupSearchDto dto, CancellationToken cancellationToken);
        Task<ApiResult<GroupListDto>> AddRoleToGroup(GroupSearchDto.AddRoleToGroupDto dto, long CreatorId, CancellationToken cancellationToken);
        Task<ApiResult<GroupListDto>> RemoveRoleFromGroup(GroupSearchDto.AddRoleToGroupDto dto, CancellationToken cancellationToken);
        Task<ApiResult<GroupListDto>> AddUserToGroup(GroupSearchDto.AddUserToGroupDto dto, long CreatorId, CancellationToken cancellationToken);
        Task<ApiResult<GroupListDto>> RemoveUserFromGroup(GroupSearchDto.AddUserToGroupDto dto, CancellationToken cancellationToken);
    }
}
