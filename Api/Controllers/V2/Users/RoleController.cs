using DataTransferObjects.DTOs.Shared;
using DataTransferObjects.DTOs.Shared.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ServicesContracts.V2.Users;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;
using X.PagedList;

namespace Api.Controllers.V2.Users
{
    [ApiVersion("2")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _RoleService;

        public RoleController(IRoleService roleService)
        {
            this._RoleService = roleService;
        }

        [Authorize(Roles = "Role-Detail,SuperAdmin")]
        [HttpPost("GetRolesFromGroupId")]
        public async Task<ApiResult<IPagedList<RoleListDto>>> GetRolesFromGroupId(RoleSearchDto dto, CancellationToken cancellationToken)
        {
            return await _RoleService.GetRolesofGroupById(dto, cancellationToken);
        }

        [Authorize(Roles = "Role-Detail,SuperAdmin")]
        [HttpPost("GetRolesFromUserId")]
        public async Task<ApiResult<IPagedList<RoleListDto>>> GetRolesFromUserId(RoleSearchDto dto, CancellationToken cancellationToken)
        {
            return await _RoleService.GetRolesofUserById(dto, cancellationToken);
        }


        [Authorize(Roles = "Role-List,SuperAdmin")]
        [HttpPost("Search")]
        public async Task<ApiResult<IPagedList<RoleListDto>>> Get(RoleSearchDto searchDto,
           CancellationToken cancellationToken)
        {
            return await _RoleService.Get(searchDto, cancellationToken);
        }

        [Authorize(Roles = "Role-Create,SuperAdmin")]
        [HttpPost]
        public virtual async Task<ApiResult<RoleListDto>> Create(RoleCuDto dto, CancellationToken cancellationToken)
        {
            return await _RoleService.CreateRole(dto, cancellationToken);
        }

        [Authorize(Roles = "Role-Edit,SuperAdmin")]
        [HttpPut]
        public virtual async Task<ApiResult<RoleListDto>> Update(RoleCuDto dto, CancellationToken cancellationToken)
        {
            return await _RoleService.UpdateRole(dto, cancellationToken);
        }

        [Authorize(Roles = "Role-Delete,SuperAdmin")]
        [HttpDelete("{id}")]
        public virtual async Task<ApiResult> Delete(string id, CancellationToken cancellationToken)
        {
            return await _RoleService.RemoveRoleById(id, cancellationToken);
        }
    }
}
