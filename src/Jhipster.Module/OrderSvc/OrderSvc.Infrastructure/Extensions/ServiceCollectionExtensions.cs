using InteractiveSvc.Infrastructure.Persistences;
using Jhipster.Infrastructure.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderSvc.Application.Configurations.Mappers;
using OrderSvc.Domain.Abstractions;

namespace Modular.OrderSvc.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<OrderSvcDbContext>(config)
                .AddScoped<IOrderSvcDbContext>(provider => provider.GetService<OrderSvcDbContext>());
            // Đăng kí mediatR
            //     services.AddMediatR(typeof(EventCreateCommand).Assembly);
            services.AddAutoMapper(typeof(AutoMapperProfile));
            // Đăng kí repository
            //   services.AddScoped(typeof(IFrameRepository), typeof(OrderRepository));

            return services;
        }

    }
}
