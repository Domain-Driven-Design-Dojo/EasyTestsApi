using AutoMapper;
using Common;
using Common.Exceptions;
using Common.Utilities;
using Data.Contracts;
using DataTransferObjects.DTOs.Shared;
using Entities.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Extensions;
using Services.ServicesContracts.BaseServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;
using DataTransferObjects.DTOs.Shared.Users;
using DataTransferObjects.DTOs.Shared.System;
using Services.ServicesContracts.V1.Users;

namespace Services.Services.V1.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UsersService> _logger;
        private readonly IJwtService _jwtService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<AccRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRepository<AccRole> _roleRepository;
        private readonly IMapper _mapper;

        public UsersService(IUserRepository userRepository, ILogger<UsersService> logger, IJwtService jwtService,
            UserManager<ApplicationUser> userManager, RoleManager<AccRole> roleManager, SignInManager<ApplicationUser> signInManager,
            IRepository<AccRole> roleRepository
            , IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _jwtService = jwtService;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<ApplicationUser>> GetAllUsers(CancellationToken cancellationToken)
        {
            return await _userRepository.TableNoTracking.ToListAsync(cancellationToken);
        }
        public async Task<AccRole> CreateRole(AccRole role, CancellationToken cancellationToken)
        {
            _logger.LogError("متد Create فراخوانی شد");

            var exists = await _roleManager.FindByNameAsync(role.Name);
            if (exists != null)
                throw new BadRequestException(ApiResultStatusCode.NameIsExists);

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return role;

            throw new BadRequestException(result.Errors);
        }


        public async Task<ApplicationUser> CreateUser(ApplicationUser user, CancellationToken cancellationToken)
        {
            //_logger.LogError("متد Create فراخوانی شد");
            //HttpContext.RiseError(new Exception("متد Create فراخوانی شد"));

            var exists = await _userManager.FindByNameAsync(user.UserName);
            if (exists != null)
                throw new BadRequestException(ApiResultStatusCode.NameIsExists);

            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (!result.Succeeded)
                throw new BadRequestException(result.Errors);


            return await _userManager.FindByNameAsync(user.UserName);
        }

        public async Task<AccessToken> Login(string userName, string password, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByNameAsync(userName);

            if (existingUser == null || existingUser.IsActive == false)
                throw new BadRequestException(ApiResultStatusCode.UserNameOrPasswordIsWrong.ToDisplay());

            //var result = await _signInManager.PasswordSignInAsync(userName, password, false, false);
            var result = await _signInManager.CheckPasswordSignInAsync(existingUser, password, false);

            if (!result.Succeeded)
                throw new BadRequestException(ApiResultStatusCode.UserNameOrPasswordIsWrong.ToDisplay());

            var roles = await _userManager.GetRolesAsync(existingUser);

            var cacheUser = AuthorizationCache.ActiveUsersRoles.Where(x => x.UserId == existingUser.Id).FirstOrDefault();

            if (cacheUser is null)
            {
                AuthorizationCache.ActiveUsersRoles.Add(new UsersRole
                {
                    UserId = existingUser.Id,
                    Roles = roles?.ToList()
                });
            }
            else
                cacheUser.Roles = roles?.ToList();

            var jwt = await _jwtService.GenerateAsync(existingUser);
            jwt.roles = roles;
            jwt.PersonId = existingUser.FPersonsId;
            return jwt;
        }

        //public async Task<ApplicationUser> AddRoleToUser(long userId, string roleName, CancellationToken cancellationToken)
        public async Task<ApplicationUser> AddRoleToUser(long userId, long roleId, CancellationToken cancellationToken)
        {
            var existingUser = _userRepository.GetByIdAsync(cancellationToken, userId);
            if (existingUser.Result == null)
                throw new BadRequestException(ApiResultStatusCode.NotFound.ToDisplay());

            //var existingRole = _roleManager.FindByNameAsync(roleName);
            var existingRole = await _roleManager.FindByIdAsync(roleId.ToString());
            if (existingRole == null)
                throw new BadRequestException(ApiResultStatusCode.NotFound.ToDisplay());

            var result = await _userManager.AddToRoleDbAndCache(existingUser.Result, existingRole.Name);
            if (!result.Succeeded)
                throw new BadRequestException(ApiResultStatusCode.UserNameOrPasswordIsWrong.ToDisplay());

            return existingUser.Result;
        }

        public async Task<ApplicationUser> GetUserByUserName(ApplicationUser user, CancellationToken cancellationToken)
        {
            var exists = await _userManager.FindByNameAsync(user.UserName);
            if (exists == null)
                throw new BadRequestException(ApiResultStatusCode.NameIsExists);

            return await _userManager.FindByNameAsync(user.UserName);
        }

        public Task<ApiResult<IPagedList<UserListDto>>> Search(UserSearchDto searchDto, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }



    }
}
