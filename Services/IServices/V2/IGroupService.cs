using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects.DataTransferObjects.AccountsDTOs;
using DataTransferObjects.DataTransferObjects.UserDTOs;
using DataTransferObjects.SharedModels;
using X.PagedList;

namespace Services.IServices.V2
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
