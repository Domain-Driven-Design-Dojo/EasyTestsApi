//using DataTransferObjects.DataTransferObjects.ConfigDTOs;
//using DataTransferObjects.GlobalDtos;
//using Entities.DatabaseModels.UserModels;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.DependencyInjection;
//using Services.IServices.V2;
//using Services.Services.V2;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace WebFramework.Configuration
//{
//    public static class SysConfigExtentions
//    {
//        public async static Task<IApplicationBuilder> SysConfigInitialize(this IApplicationBuilder app, ConfigListDto configListDto)
//        {
//            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
//            var sysConfigServices = (IConfigService)scope.ServiceProvider.GetServices<IConfigService>().FirstOrDefault();
//            var sysConfig = sysConfigServices.getActiveSysConfig();
//            var cancellationTokenSource = new CancellationTokenSource();

//            if (sysConfig is null)
//            {
//                sysConfig = await sysConfigServices.updateActiveSysConfigAsync(cancellationTokenSource.Token);
//            }
//            configListDto = sysConfig;

//            return app;
//        }
//        public static void AddSysConfig(this IServiceCollection services)
//        {
//            services.AddScoped<IConfigService, ConfigService>();

//        }
//    }
//}
