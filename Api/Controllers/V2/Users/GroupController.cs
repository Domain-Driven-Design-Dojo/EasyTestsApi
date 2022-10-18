using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using DataTransferObjects.DataTransferObjects.AccountsDTOs;
using DataTransferObjects.DataTransferObjects.UserDTOs;
using DataTransferObjects.SharedModels;
using Entities.DatabaseModels.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;
using Services.IServices.V2;
using Services.Services.V2;
using WebFramework.Api;
using X.PagedList;

namespace Api.Controllers.V2.Users
{

    [ApiVersion("2")]
    public class GroupController : CrudControllerGenericWithRole<GroupCuDto, GroupListDto, GroupSearchDto, AccGroup, int>
    {
        private readonly ICrudService<GroupCuDto, GroupListDto, GroupSearchDto, AccGroup, int> _service;
        private readonly IGroupService _GroupService;

        public GroupController(IGroupService groupService,
             ICrudService<GroupCuDto, GroupListDto, GroupSearchDto, AccGroup, int> service) : base(service, "Group")
        {
            _service = service;
            this._GroupService = groupService;
        }

        [Authorize(Roles = "SuperAdmin,Group-Detail")]
        [HttpPost("GetGroupsFromRoleId")]
        public async Task<ApiResult<IPagedList<GroupListDto>>> GetGroupsFromRoleId(GroupSearchDto dto, CancellationToken cancellationToken)
        {
            return await _GroupService.GetGroupsByRoleId(dto, cancellationToken);
        }

        [Authorize(Roles = "SuperAdmin,Group-Detail")]
        [HttpPost("GetGroupsFromUserId")]
        public async Task<ApiResult<IPagedList<GroupListDto>>> GetGroupsFromUserId(GroupSearchDto dto, CancellationToken cancellationToken)
        {
            return await _GroupService.GetGroupsByUserId(dto, cancellationToken);
        }


        [Authorize(Roles = "SuperAdmin,Role-EditUserRoles")]
        [HttpPost("AddRoleToGroup")]
        public async Task<ApiResult<GroupListDto>> AddRoleToGroup(GroupSearchDto.AddRoleToGroupDto dto,
           CancellationToken cancellationToken)
        {
            return await _GroupService.AddRoleToGroup(dto, User.Identity.GetUserId(), cancellationToken);
        }


        [Authorize(Roles = "SuperAdmin,Role-EditUserRoles")]
        [HttpPost("RemoveRoleFromGroup")]
        public async Task<ApiResult<GroupListDto>> RemoveRoleFromGroup(GroupSearchDto.AddRoleToGroupDto dto,
           CancellationToken cancellationToken)
        {
            return await _GroupService.RemoveRoleFromGroup(dto, cancellationToken);
        }

        [Authorize(Roles = "SuperAdmin,Role-EditUserRoles,Group-EditUserGroup")]
        [HttpPost("AddUserToGroup")]
        public async Task<ApiResult<GroupListDto>> AddUserToGroup(GroupSearchDto.AddUserToGroupDto dto,
           CancellationToken cancellationToken)
        {
            return await _GroupService.AddUserToGroup(dto, User.Identity.GetUserId(), cancellationToken);
        }
        [Authorize(Roles = "SuperAdmin,Role-EditUserRoles,Group-EditUserGroup")]
        [HttpPost("RemoveUserFromGroup")]
        public async Task<ApiResult<GroupListDto>> RemoveUserFromGroup(GroupSearchDto.AddUserToGroupDto dto,
           CancellationToken cancellationToken)
        {
            return await _GroupService.RemoveUserFromGroup(dto, cancellationToken);
        }
    }
}
