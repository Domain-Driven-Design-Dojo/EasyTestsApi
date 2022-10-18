using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects.DataTransferObjects.UserDTOs;
using DataTransferObjects.SharedModels;
using X.PagedList;

namespace Services.IServices.V2
{
    public interface IRoleService
    {
        Task<ApiResult<RoleListDto>> CreateRole(RoleCuDto roleCuDto, CancellationToken cancellationToken);
        Task<ApiResult<RoleListDto>> UpdateRole(RoleCuDto roleCuDto, CancellationToken cancellationToken);
        Task<ApiResult<RoleListDto>> RemoveRoleById(string id, CancellationToken cancellationToken);
        Task<ApiResult<IPagedList<RoleListDto>>> Get(RoleSearchDto searchDto, CancellationToken cancellationToken);
        Task<ApiResult<IList<RoleListDto>>> GetAll(RoleSearchDto searchDto, CancellationToken cancellationToken);
        Task<ApiResult<IPagedList<RoleListDto>>> GetRolesofGroupById(RoleSearchDto dto, CancellationToken cancellationToken);
        Task<ApiResult<IPagedList<RoleListDto>>> GetRolesofUserById(RoleSearchDto dto, CancellationToken cancellationToken);
    }
}
