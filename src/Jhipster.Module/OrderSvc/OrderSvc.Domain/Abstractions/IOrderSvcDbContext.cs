using Microsoft.EntityFrameworkCore;
using OrderSvc.Domain.Entities;

namespace OrderSvc.Domain.Abstractions
{
    public interface IOrderSvcDbContext
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
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
