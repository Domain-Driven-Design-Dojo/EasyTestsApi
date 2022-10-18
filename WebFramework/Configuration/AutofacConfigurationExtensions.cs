using Autofac;
using Common;
using Data;
using Data.Contracts;
using Data.Repositories;
using Entities.DatabaseModels.CommonModels.BaseModels;
using Services;
using Services.IServices;
using Services.IServices.V2;
using Services.Services;
using Services.Services.V2;

namespace WebFramework.Configuration
{
    public static class AutofacConfigurationExtensions
    {
        public static void AddServices(this ContainerBuilder containerBuilder)
        {
            //RegisterType > As > LifeTime
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(RepositoryWithActors<,>)).As(typeof(IRepositoryWithActors<,>)).InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(ReportRepository<,>)).As(typeof(IReportRepository<,>)).InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(CrudService<,,,,>)).As(typeof(ICrudService<,,,,>)).InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(ReportService<,,,>)).As(typeof(IReportService<,,,>)).InstancePerLifetimeScope();
            //containerBuilder.RegisterGeneric(typeof(FinanceService)).As(typeof(IFinanceService)).InstancePerLifetimeScope();

            var commonAssembly = typeof(SiteSettings).Assembly;
            var entitiesAssembly = typeof(IEntity).Assembly;
            var dataAssembly = typeof(ApplicationDbContext).Assembly;
            var servicesAssembly = typeof(JwtService).Assembly;

            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly, servicesAssembly)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly, servicesAssembly)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly, servicesAssembly)
                .AssignableTo<ISingletonDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();

            
        }
    }
}
