using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using DataTransferObjects.DataTransferObjects.AccountsDTOs;
using DataTransferObjects.DataTransferObjects.UserDTOs;
using DataTransferObjects.SharedModels;
using Entities.DatabaseModels.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices.V2;
using WebFramework.Api;
using X.PagedList;

namespace Api.Controllers.V2.Users
{
    [ApiVersion("2")]
    public class UsersController : BaseController
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }


        [Authorize(Roles = "User-Detail,SuperAdmin")]
        [HttpPost("GetUsersFromGroupId")]
        public async Task<ApiResult<IPagedList<ApplicationUserListDto>>> GetUsersFromGroupId(ApplicationUserSearchDto dto, CancellationToken cancellationToken)
        {
            return await _userService.GetUsersByGroupId(dto, cancellationToken);
        }


        [Authorize(Roles = "User-Edit,SuperAdmin")]
        [HttpPut]
        public virtual async Task<ApiResult<ApplicationUserListDto>> Update(ApplicationUserCuDto dto, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUser(dto, cancellationToken);
        }

        [Authorize(Roles = "User-List,SuperAdmin")]
        [HttpPost("Search")]
        public async Task<ApiResult<IPagedList<ApplicationUserListDto>>> Get(ApplicationUserSearchDto searchDto,
            CancellationToken cancellationToken)
        {
            return await _userService.Get(searchDto, cancellationToken);
        }


        [Authorize(Roles = "User-Detail,SuperAdmin")]
        [HttpPost("GetUsersByNationalId")]
        public async Task<ApiResult<List<ApplicationUserListDto>>> GetUsersByNationalId(ApplicationUserSearchDto userDto, CancellationToken cancellationToken)
        {
            var result = await _userService.GetUsersByNationalId(userDto, cancellationToken);

            return result;
        }


        [HttpPost("ChangePasswordByUser")]
        public async Task<ApiResult<ApplicationUserListDto>> ChangePasswordByUser(
            ApplicationUserSearchDto.ApplicationUserChangePasswordDto dto,
            CancellationToken cancellationToken)
        {
            return await _userService.ChangePassword(dto, false, cancellationToken);
        }

        [Authorize(Roles = "User-ChangePasswordByAdmin,SuperAdmin")]
        [HttpPost("ChangePasswordByAdmin")]
        public async Task<ApiResult<ApplicationUserListDto>> ChangePasswordByAdmin(
            ApplicationUserSearchDto.ApplicationUserChangePasswordDto dto,
            CancellationToken cancellationToken)
        {
            return await _userService.ChangePassword(dto, true, cancellationToken);
        }

        [Authorize(Roles = "User-Create,SuperAdmin")]
        [HttpPost]
        public async Task<ApiResult<ApplicationUserListDto>> Create(
            ApplicationUserCuDto dto,
            CancellationToken cancellationToken)
        {
            return await _userService.CreateUser(dto, cancellationToken);
        }

        //[HttpPost]
        //public async Task<AccessToken> Login(string userName, string password, CancellationToken cancellationToken)
        //{
        //    return await _userService.Login(userName,password, cancellationToken);
        //}

        [HttpPost("[action]")]
        [AllowAnonymous]
        public virtual async Task<AccessToken> Token(
            //[FromBody] TokenRequest bodyRequest, [FromForm] TokenRequest formRequest, CancellationToken cancellationToken)
            TokenRequest tokenRequest, CancellationToken cancellationToken)
        {
            tokenRequest.Username ??= Request.Form[nameof(tokenRequest.Username)].ToString();
            //tokenRequest.Username ??= Request.Body[nameof(tokenRequest.Username)].ToString();
            tokenRequest.Password ??= Request.Form[nameof(tokenRequest.Password)].ToString();
            tokenRequest.grant_type ??= Request.Form[nameof(tokenRequest.grant_type)].ToString();
            //TokenRequest tokenRequest = bodyRequest ?? formRequest;

            if (!tokenRequest.grant_type.Equals("password", StringComparison.OrdinalIgnoreCase))
                throw new Exception("OAuth flow is not password.");

            //var user = await userRepository.GetByUserAndPass(username, password, cancellationToken);
            return await _userService.Login(tokenRequest.Username, tokenRequest.Password, cancellationToken);
        }

        //Currently, this method is just being used by Swagger
        [HttpPost("TokenFromForm")]
        [AllowAnonymous]
        public virtual async Task<ActionResult> TokenFromForm([FromForm] TokenRequest tokenRequest, CancellationToken cancellationToken)
        {
            if (!tokenRequest.grant_type.Equals("password", StringComparison.OrdinalIgnoreCase))
                throw new Exception("OAuth flow is not password.");

            var token = await _userService.Login(tokenRequest.Username, tokenRequest.Password, cancellationToken);
            return new JsonResult(token);
        }

        [Authorize(Roles = "Role-EditUserRoles,SuperAdmin")]
        [HttpPost("AddRoleToUser")]
        public async Task<ApiResult<ApplicationUserListDto>> AddRoleToUser(ApplicationUserSearchDto.AddRoleToUserDto dto,
            CancellationToken cancellationToken)
        {
            return await _userService.AddRoleToUser(dto.UserId, dto.RoleId, cancellationToken);
        }


        [Authorize(Roles = "User-Detail,SuperAdmin")]
        [HttpPost("GetUsersFromRoleId")]
        public async Task<ApiResult<IPagedList<ApplicationUserListDto>>> GetUsersFromRoleId(ApplicationUserSearchDto dto,CancellationToken cancellationToken)
        {
            return await _userService.GetUsersByRoleId(dto, cancellationToken);
        }

        [Authorize(Roles = "Role-EditUserRoles,SuperAdmin")]
        [HttpPost("RemoveRoleFromUser")]
        public async Task<ApiResult<ApplicationUserListDto>> RemoveRoleFromUser(ApplicationUserSearchDto.AddRoleToUserDto dto,
            CancellationToken cancellationToken)
        {
            return await _userService.RemoveRoleFromUser(dto.UserId, dto.RoleId, cancellationToken);
        }

        [Authorize]
        [HttpPost("WhoAmI")]
        public async Task<ApiResult<ApplicationUserListDto>> WhoAmI(CancellationToken cancellationToken)
        {
            var currentUserId = User.Identity.GetUserId();
            return await _userService.GetUserById(currentUserId, cancellationToken);
        }


    }
}
