using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects.DTOs.Shared;
using DataTransferObjects.DTOs.Shared.Users;
using X.PagedList;

namespace Services.ServicesContracts.V2.Users
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
