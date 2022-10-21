//using Api.Models.GlobalVariables;
using Autofac;
using Common;
using DataTransferObjects.CustomMapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Services.IServices;
using Services.IServices.V2;
using Services.Services;
using Services.Services.V1;
using Services.Services.V2;
using System.Collections.Generic;
using WebFramework.Configuration;
using WebFramework.Middlewares;
using WebFramework.Swagger;
using System;
using Api.Utilities;
using System.Threading;

namespace Api
{
    public class Startup
    {
        private readonly SiteSettings _siteSetting;
        public IConfiguration Configuration { get; }



        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.InitializeAutoMapper();

            services.AddDbContext(Configuration);

            services.AddCustomIdentity(_siteSetting.IdentitySettings);

            ////Add our IFileServerProvider implementation as a singleton
            //services.AddSingleton<IFileServerProvider>(new FileServerProvider(
            //    new List<FileServerOptions>
            //    {
            //        new FileServerOptions
            //        {
            //            FileProvider = new PhysicalFileProvider(Configuration["SiteSettings:Files:ImagesPhysicalLocation"]),
            //            RequestPath = Configuration["SiteSettings:Files:ImagesPath"],
            //            EnableDirectoryBrowsing = false
            //        }
            //        ,
            //        new FileServerOptions
            //        {
            //            FileProvider = new PhysicalFileProvider(Configuration["SiteSettings:Files:DocsPhysicalLocation"]),
            //            RequestPath = Configuration["SiteSettings:Files:DocsPath"],
            //            EnableDirectoryBrowsing = false
            //        }
            //    }));
            //DataTransferObjects.GlobalDtos.Configs.ImagesPath = Configuration["SiteSettings:Files:ImagesPath"];
            //DataTransferObjects.GlobalDtos.Configs.TicketAttachmentsPath = Configuration["SiteSettings:Files:TicketAttachmentsPath"];
            //DataTransferObjects.GlobalDtos.Configs.DocsPath = Configuration["SiteSettings:Files:DocsPath"];

            services.AddMinimalMvc();

            //services.AddElmahCore(Configuration, _siteSetting);

            services.AddJwtAuthentication(_siteSetting.JwtSettings);

            services.AddCustomApiVersioning();

            //services.AddControllers();
            services.AddSwagger();

            services.AddScoped<Services.IServices.IUsersService, Services.Services.V1.UsersService>();
            services.AddScoped<Services.IServices.V2.IUsersService, Services.Services.V2.UsersService>();
            services.AddScoped<IIndividualsService, IndividualsService>();
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<Services.IServices.V2.IPeopleTypeService, Services.Services.V2.PeopleTypeService>();
            services.AddScoped<Services.IServices.V2.IRoleService, Services.Services.V2.RoleService>();
            services.AddScoped<Services.IServices.V2.IPersonService, Services.Services.V2.PersonService>();

            //services.AddSysConfig();

            ////services.AddScoped<IMapper, Mapper>();
            //services.AddScoped<IRepositoryWithActors<,>, RepositoryWithActors<,>>();
            // Don't create a ContainerBuilder for Autofac here, and don't call builder.Populate()
            // That happens in the AutofacServiceProviderFactory for you.

        }

        // ConfigureContainer is where you can register things directly with Autofac. 
        // This runs after ConfigureServices so the things ere will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {

            //Register Services to Autofac ContainerBuilder
            builder.AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            //IFileServerProvider fileServerprovider,
             IServiceProvider serviceProvider
           )
        {
            //app.IntializeDatabase();
            //app.SysConfigInitialize(GlobalVariables.configDto).Wait();
            app.AuthorizationInitialize().Wait();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseCustomExceptionHandler();
            app.UseHsts(env);
            app.UseHttpsRedirection();
            //app.UseElmahCore(_siteSetting);
            app.UseSwaggerAndUI();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //Use this config just in Develoment (not in Production)
            //app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseEndpoints(config =>
            {
                config.MapControllers(); // Map attribute routing
                //    .RequireAuthorization(); Apply AuthorizeFilter as global filter to all endpoints
                //config.MapDefaultControllerRoute(); // Map default route {controller=Home}/{action=Index}/{id?}
            });
            //https://stackoverflow.com/questions/43190824/get-physicalpath-after-setting-up-app-usefileserver-in-asp-net-core
            //call convenience method which adds our FileServerOptions from 
            // the IFileServerProvider service
            //app.UseFileServerProvider(fileServerprovider);

            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new MyAuthorizationFilter() }
            //});

            //Using 'UseMvc' to configure MVC is not supported while using Endpoint Routing.
            //To continue using 'UseMvc', please set 'MvcOptions.EnableEndpointRouting = false' inside 'ConfigureServices'.
            //app.UseMvc();
        }
    }
}
