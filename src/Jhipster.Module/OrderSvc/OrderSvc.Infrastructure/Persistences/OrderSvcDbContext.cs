using Jhipster.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using OrderSvc.Domain.Abstractions;
using OrderSvc.Domain.Entities;
using Org.BouncyCastle.Asn1.Mozilla;

namespace InteractiveSvc.Infrastructure.Persistences
{
    public class OrderSvcDbContext : ModuleDbContext, IOrderSvcDbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<CategoryProduct> categoriesProduct { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<PackageMember> packageMembers { get; set; }
        public DbSet<ProfileCustomer> profileCustomer { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<VoucherProductCustomer> voucherProductsCustomer { get; set; }
        public DbSet<Voucher> vouchers { get; set; }
        public DbSet<Affiliates> affiliates { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

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
