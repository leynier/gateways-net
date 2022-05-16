using Gateways.Business.Contracts;
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

            services.AddScoped<IObjectContext, ObjectContext>();

            // Services
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IGatewayService, GatewayService>();

            // Repositories
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IGatewayRepository, GatewayRepository>();
        }
    }
}