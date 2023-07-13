using Jhipster.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using OrderSvc.Domain.Abstractions;

namespace InteractiveSvc.Infrastructure.Persistences
{
    public class OrderSvcDbContext : ModuleDbContext, IOrderSvcDbContext
    {
        public OrderSvcDbContext(DbContextOptions<OrderSvcDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(true, cancellationToken);
        }


    }
}
