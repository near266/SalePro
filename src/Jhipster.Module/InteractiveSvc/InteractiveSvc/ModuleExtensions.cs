using InteractiveSvc.Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Ordering.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveSvc
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
