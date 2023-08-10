using Jhipster.Domain;
using Jhipster.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using OrderSvc.Domain.Entities;

namespace Jhipster.Infrastructure.Data
{
    public class ApplicationDatabaseContext : IdentityDbContext<
        User, Domain.Role, string,
        IdentityUserClaim<string>,
        UserRole,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>
    >
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

   

        #region Order
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

        public DbSet<CategoryProduct> categoriesProduct { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<PackageMember> packageMembers { get; set; }
        public DbSet<ProfileCustomer> profileCustomer { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<VoucherProductCustomer> voucherProductsCustomer { get; set; }
        public DbSet<Voucher> vouchers { get; set; }
        public DbSet<Affiliates> affiliates { get; set; }
        public DbSet<InfoPackageMember> infoPackages { get; set; }


        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserRole>().Ignore(c => c.User);
            // Rename AspNet default tables
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Domain.Role>().ToTable("Roles");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<User>()
                .HasMany(e => e.UserRoles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }

        /// <summary>
        /// SaveChangesAsync with entities audit
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entries = ChangeTracker
              .Entries()
              .Where(e => e.Entity is IAuditedEntityBase && (
                  e.State == EntityState.Added
                  || e.State == EntityState.Modified));

            string modifiedOrCreatedBy = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "System";

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((IAuditedEntityBase)entityEntry.Entity).CreatedDate = DateTime.Now;
                    ((IAuditedEntityBase)entityEntry.Entity).CreatedBy = modifiedOrCreatedBy;
                }
                else
                {
                    Entry((IAuditedEntityBase)entityEntry.Entity).Property(p => p.CreatedDate).IsModified = false;
                    Entry((IAuditedEntityBase)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }
              ((IAuditedEntityBase)entityEntry.Entity).LastModifiedDate = DateTime.Now;
                ((IAuditedEntityBase)entityEntry.Entity).LastModifiedBy = modifiedOrCreatedBy;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
