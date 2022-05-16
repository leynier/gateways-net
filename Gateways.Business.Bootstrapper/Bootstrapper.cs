using Gateways.Business.Contracts;
using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.Services;
using Gateways.Business.Implementations.Repositories;
using Gateways.Business.Implementations.Services;
using Gateways.Data.Implementations;
using Gateways.Data.Implementations.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gateways.Business.Bootstrapper
{
    public static class Bootstrapper
    {
        public static void Boot(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddDbContext<ObjectContext>(options =>
            {
                options.UseInMemoryDatabase(configuration["DatabaseName"]);
            });

            var abstracts = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.Namespace == "Gateways.Business.Contracts.Services")
                .Where(x => x.IsInterface)
                .Where(x => !x.ContainsGenericParameters)
                .ToList();
            foreach (var item in abstracts)
                BootType(services, item);
        }

        private static void BootType(IServiceCollection services, Type type)
        {
            if (services.Any(x => x.ServiceType == type))
                return;
            var impls = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsAssignableTo(type))
                .Where(x => x.IsClass)
                .Where(x => !x.IsAbstract)
                .ToList();
            if (impls.Count > 1)
                throw new Exception($"{type.Name} has more than one implementation");
            if (impls.Count == 0)
                return;
            var impl = impls.First();
            services.AddScoped(type, impl);
            var parameterTpes = impl
                .GetConstructors()
                .SelectMany(t => t.GetParameters())
                .Select(p => p.ParameterType)
                .ToList();
            foreach (var parameterType in parameterTpes)
                BootType(services, parameterType);
        }
    }
}