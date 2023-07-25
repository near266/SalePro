using InteractiveSvc.Infrastructure.Persistences;
using Jhipster.Infrastructure.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderSvc.Application.Command.ProductCommand;
using OrderSvc.Application.Configurations.Mappers;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Abstractions;
using OrderSvc.Infrastructure.Persistences.Repositories;
using System.Reflection;

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
            services.AddMediatR(typeof(AddProductCommand).GetTypeInfo().Assembly);
            services.AddAutoMapper(typeof(AutoMapperProfile));
            // Đăng kí repository
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(ICompanyReqository), typeof(CompanyRepository));
            services.AddScoped(typeof(IPackageMember), typeof(PackgeMemberRepository));
            services.AddScoped(typeof(IProductRepository), typeof(ProductionRepository));
            services.AddScoped(typeof(ITransactionRepository), typeof(TransactionRepository));
            services.AddScoped(typeof(IVoucherRepository), typeof(VoucherRepository));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));



            return services;
        }

    }
}
