using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects.GlobalDtos;
using Entities.DatabaseModels.UserModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace WebFramework.Configuration
{
    public static class AuthorizationExtensions
    {
        public async static Task<IApplicationBuilder> AuthorizationInitialize(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var UserManager = (UserManager<ApplicationUser>)scope.ServiceProvider.GetServices<UserManager<ApplicationUser>>().FirstOrDefault();
            await AuthorizationCache.InitializeCache(UserManager);
            return app;
        }


    }
}
