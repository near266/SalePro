using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modular.OrderSvc.Infrastructure.Extensions;
using OrderSvc.Domain.Extensions;

namespace OrderSvc
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddInteractiveModule(this IServiceCollection services, IConfiguration configuration)
        {
                services.AddBaseCore()
                 .AddBaseInfrastructure(configuration);

            return services;
        }

    }
}
