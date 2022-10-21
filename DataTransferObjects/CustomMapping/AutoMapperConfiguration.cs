using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DataTransferObjects.CustomMapping
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
        {
            //With AutoMapper Instance, you need to call AddAutoMapper services and pass assemblies that contains automapper Profile class
            //services.AddAutoMapper(assembly1, assembly2, assembly3);
            //See http://docs.automapper.org/en/stable/Configuration.html
            //And https://code-maze.com/automapper-net-core/

            services.AddAutoMapper(config =>
            {
                config.AddCustomMappingProfile();
                //AddCustomMappingProfile((IMapperConfigurationExpression)config);

                config.Advanced.BeforeSeal(configProvider =>
                {
                    configProvider.CompileMappings();
                });

            }, assemblies);

            #region Deprecated (Use AutoMapper Instance instead)
            //Mapper.Initialize(config =>
            //{
            //    config.AddCustomMappingProfile();
            //});

            ////Compile mapping after configuration to boost map speed
            //Mapper.Configuration.CompileMappings();
            #endregion
        }

        public static void AddCustomMappingProfile(this IMapperConfigurationExpression config)
        {
            //Before:
            //config.AddCustomMappingProfile(Assembly.GetEntryAssembly());
            //After: The Entry Assembly does not export dto types so dto mappings are not created. by changing to below code now it works correctly.
            config.AddCustomMappingProfile(Assembly.GetExecutingAssembly());
        }

        public static void AddCustomMappingProfile(this IMapperConfigurationExpression config, params Assembly[] assemblies)
        {
            var allTypes = assemblies.SelectMany(a => a.ExportedTypes);

            var list = allTypes.Where(type => type.IsClass && !type.IsAbstract &&
                type.GetInterfaces().Contains(typeof(IHaveCustomMapping)))
                .Select(type => (IHaveCustomMapping)Activator.CreateInstance(type));

            var profile = new CustomMappingProfile(list);

            config.AddProfile(profile);
        }
    }
}
