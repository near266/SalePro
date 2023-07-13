using InteractiveSvc.Application.Commands.Events;
using InteractiveSvc.Application.Configurations.Mappers;
using InteractiveSvc.Application.Persistences;
using InteractiveSvc.Domain.Abstractions;
using InteractiveSvc.Infrastructure.Persistences;
using InteractiveSvc.Infrastructure.Persistences.Repositories;
using Jhipster.Infrastructure.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Module.Ordering.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<InteractiveSvcDbContext>(config)
                .AddScoped<IInteractiveSvcDbContext>(provider => provider.GetService<InteractiveSvcDbContext>());
            // Đăng kí mediatR
            services.AddMediatR(typeof(EventCreateCommand).Assembly);
            services.AddAutoMapper(typeof(AutoMapperProfile));
            // Đăng kí repository
            services.AddScoped(typeof(IFrameRepository), typeof(FrameRepository));

            return services;
        }

    }
}
