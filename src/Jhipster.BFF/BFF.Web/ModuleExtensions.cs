using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BFF.Web
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddBFFWebModule(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
