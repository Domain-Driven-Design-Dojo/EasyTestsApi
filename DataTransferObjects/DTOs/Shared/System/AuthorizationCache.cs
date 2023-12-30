using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.UserModels;
using Microsoft.AspNetCore.Identity;

namespace DataTransferObjects.DTOs.Shared.System
{
    public static class AuthorizationCache
    {
        public static List<UsersRole> ActiveUsersRoles { get; set; }

        public static async Task InitializeCache(UserManager<ApplicationUser> userManager)
        {
            ActiveUsersRoles = new List<UsersRole>();
            foreach (var item in userManager.Users.Where(src => src.IsActive).ToList())
            {
                var Roles = await userManager.GetRolesAsync(item);
                ActiveUsersRoles.Add(new UsersRole()
                {
                    UserId = item.Id,
                    Roles = Roles.ToList()
                });

            }
        }

    }

    public class UsersRole
    {
        public long UserId { get; set; }
        public List<string> Roles { get; set; }

    }
}
