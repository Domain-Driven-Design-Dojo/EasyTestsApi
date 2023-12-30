using Api.Models.InputModels.V1.Users;
using AutoMapper;
using Common;
using Common.Exceptions;
using Common.Utilities;
using DataTransferObjects.DTOs.Shared;
using DataTransferObjects.DTOs.Shared.Users;
using Entities.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.ServicesContracts.V1.Users;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Api.Controllers.V1.Users
{
    [ApiVersion("1")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUsersService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUsersService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("CreateRole")]
        [AllowAnonymous]
        public async Task<RoleDto> CreateRole(RoleDto roleDto, CancellationToken cancellationToken)
        {
            //var UserId = User.Identity.GetUserId();

            AccRole role = new AccRole();
            _mapper.Map(roleDto, role);

            var result = _userService.CreateRole(role, cancellationToken);
            if (result.Result != null)
                return _mapper.Map(result.Result, roleDto);

            throw new BadRequestException(ApiResultStatusCode.RoleCreationFailed.ToDisplay());

        }
        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public async Task<ApplicationUserDto> CreateUser(ApplicationUserDto userDto, CancellationToken cancellationToken)
        {

            userDto.CreationDate = DateTime.Now;
            userDto.LastLoginDate = DateTime.Now;
            userDto.ModificationDate = DateTime.Now;

            ApplicationUser user = new ApplicationUser();
            _mapper.Map(userDto, user);
            user.PasswordHash = userDto.Password;

            var result = _userService.CreateUser(user, cancellationToken);
            if (result.Result != null)
                return _mapper.Map(result.Result, userDto);

            throw new BadRequestException(ApiResultStatusCode.RoleCreationFailed.ToDisplay());
        }

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


        [HttpPost("[action]")]
        [AllowAnonymous]
        //public async Task<UserDto> AddRoleToUser(long userId, RoleDto roleDto, CancellationToken cancellationToken)
        public async Task<UserDto> AddRoleToUser(AddRoleToUserInput input, CancellationToken cancellationToken)
        {

            var result = await _userService.AddRoleToUser(input.UserId, input.RoleId, cancellationToken);

            return _mapper.Map(result, new UserDto());
        }

        [HttpPost("GetUserByUserName")]
        public async Task<ApiResult<ApplicationUserDto>> GetUserByUserName(ApplicationUserDto userDto, CancellationToken cancellationToken)
        {

            ApplicationUser user = new ApplicationUser();
            _mapper.Map(userDto, user);
            var result = await _userService.GetUserByUserName(user, cancellationToken);

            return _mapper.Map(result, new ApplicationUserDto());
        }
    }
}
