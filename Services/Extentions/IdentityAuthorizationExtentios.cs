using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects.DTOs.Shared.System;
using Entities.UserModels;
using Microsoft.AspNetCore.Identity;

namespace Services.Extensions
{
    public static class IdentityAuthorizationExtensions
    {
        public static async Task<IdentityResult> AddToRoleDbAndCache(this UserManager<ApplicationUser> userManager, ApplicationUser user, string role)
        {
            var result = await userManager.AddToRoleAsync(user, role);
            if (result.Succeeded)
            {
                var CacheUser = AuthorizationCache.ActiveUsersRoles.Where(x => x.UserId == user.Id).FirstOrDefault();
                CacheUser?.Roles.Add(role);
            }
            return result;
        }

        public static async Task<IdentityResult> RemoveFromRoleDbAndCache(this UserManager<ApplicationUser> userManager, ApplicationUser user, string role)
        {
            var result = await userManager.RemoveFromRoleAsync(user, role);
            if (result.Succeeded)
            {
                var CacheUser = AuthorizationCache.ActiveUsersRoles.Where(x => x.UserId == user.Id).FirstOrDefault();
                CacheUser?.Roles?.Remove(role);
            }
            return result;
        }

        public static async Task<IdentityResult> CreateUserDbAndCache(this UserManager<ApplicationUser> userManager, ApplicationUser user, string password)
        {
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var CreatedUser = await userManager.FindByNameAsync(user.UserName);
                AuthorizationCache.ActiveUsersRoles.Add
                 (
                 new UsersRole()
                 {
                     UserId = CreatedUser.Id,
                     Roles=new List<string>()
                 });
            }
            return result;
        }


    }
}
