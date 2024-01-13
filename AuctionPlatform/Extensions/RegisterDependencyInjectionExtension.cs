#region Using
using AuctionPlatform.Business._01_Common;
using AuctionPlatforn.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
#endregion Using

namespace AuctionPlatform.Extensions
{
    public static class RegisterDependencyInjectionExtension
    {

        public static void RegisterBusinessLayerDependencies(this IServiceCollection services)
        {
            var serviceInterfaceType = typeof(IService);

            var types = serviceInterfaceType
                        .Assembly
                        .GetExportedTypes()
                        .Where(t => t.IsClass && !t.IsAbstract)
                        .Select(t => new
                        {
                            Service = t.GetInterface($"I{t.Name}"),
                            Implementation = t
                        })
                        .Where(t => t.Service != null);

            foreach (var type in types)
            {
                services.AddTransient(type.Service, type.Implementation);
            }


            services.AddSingleton<IUrlHelper, UrlHelper>(implementationFactory =>
            {
                var actionContext =
                implementationFactory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });
        }


        public static void RegisterDataAccessLayerDependencies(this IServiceCollection services)
        {
            var repositoryInterfaceType = typeof(IGenericRepository<>);

            var types = repositoryInterfaceType
                        .Assembly
                        .GetExportedTypes()
                        .Where(t => t.IsClass && !t.IsAbstract && t.Name != "UnitOfWork")
                        .Where(x => !x.Name.Contains("GenericRepository"))
                        .Select(t => new
                        {
                            Service = t.GetInterface($"I{t.Name}"),
                            Implementation = t
                        })
                        .Where(t => t.Service != null);

            foreach (var type in types)
            {
                services.AddTransient(type.Service, type.Implementation);
            }
        }
    }
}