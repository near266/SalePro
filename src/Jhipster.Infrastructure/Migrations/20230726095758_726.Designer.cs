﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Jhipster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDatabaseContext))]
    [Migration("20230726095758_726")]
    partial class _726
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Jhipster.Domain.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Jhipster.Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<bool>("Activated")
                        .HasColumnType("boolean");

                    b.Property<string>("ActivationKey")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("activation_key");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("image_url");

                    b.Property<string>("LangKey")
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)")
                        .HasColumnName("lang_key");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ResetDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("reset_date");

                    b.Property<string>("ResetKey")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("reset_key");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Jhipster.Domain.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.Property<string>("UserId1")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId1");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.Affiliates", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ParticipantsId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ProviderId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ReferrerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SalerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("affiliates");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CategoryDescription")
                        .HasColumnType("text");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.CategoryProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("categoriesProduct");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Avatar")
                        .HasMaxLength(2147483647)
                        .HasColumnType("text");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("companies");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AffiliateId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AffiliatesId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BoughtPerson")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SalePerson")
                        .HasColumnType("uuid");

                    b.Property<string>("TransactionId")
                        .HasColumnType("text");

                    b.Property<string>("TransactionsTransactionId")
                        .HasColumnType("text");

                    b.Property<Guid?>("VoucherId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AffiliatesId");

                    b.HasIndex("TransactionsTransactionId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.PackageMember", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Decripstion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PackageName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("packageMembers");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Decripstion")
                        .HasColumnType("text");

                    b.Property<List<string>>("Image")
                        .HasColumnType("text[]");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<double?>("Price")
                        .HasColumnType("double precision");

                    b.Property<double?>("PriceNum")
                        .HasColumnType("double precision");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Provider")
                        .HasColumnType("text");

                    b.Property<string>("warranty")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("OrderId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.ProfileCustomer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Avatar")
                        .HasMaxLength(2147483647)
                        .HasColumnType("text");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Decripstion")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Position")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("profileCustomer");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.Transactions", b =>
                {
                    b.Property<string>("TransactionId")
                        .HasColumnType("text");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("text");

                    b.Property<double?>("TotalAmount")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("TransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TransactionName")
                        .HasColumnType("text");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TransactionId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CodeVoucher")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid?>("VoucherProCusId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("vouchers");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.VoucherProductCustomer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ProfileCustomerId")
                        .HasColumnType("uuid");

                    b.Property<int?>("TypeCustomer")
                        .HasColumnType("integer");

                    b.Property<Guid?>("VoucherId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProfileCustomerId");

                    b.HasIndex("VoucherId")
                        .IsUnique();

                    b.ToTable("voucherProductsCustomer");
                });

            modelBuilder.Entity("Jhipster.Domain.UserRole", b =>
                {
                    b.HasOne("Jhipster.Domain.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jhipster.Domain.User", null)
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jhipster.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Jhipster.Domain.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Jhipster.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Jhipster.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Jhipster.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.CategoryProduct", b =>
                {
                    b.HasOne("OrderSvc.Domain.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("OrderSvc.Domain.Entities.Product", "Product")
                        .WithOne("CategoryProduct")
                        .HasForeignKey("OrderSvc.Domain.Entities.CategoryProduct", "ProductId");

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.Order", b =>
                {
                    b.HasOne("OrderSvc.Domain.Entities.Affiliates", "Affiliates")
                        .WithMany()
                        .HasForeignKey("AffiliatesId");

                    b.HasOne("OrderSvc.Domain.Entities.Transactions", "Transactions")
                        .WithMany()
                        .HasForeignKey("TransactionsTransactionId");

                    b.Navigation("Affiliates");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.PackageMember", b =>
                {
                    b.HasOne("OrderSvc.Domain.Entities.ProfileCustomer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.Product", b =>
                {
                    b.HasOne("OrderSvc.Domain.Entities.Company", "Company")
                        .WithMany("Products")
                        .HasForeignKey("CompanyId");

                    b.HasOne("OrderSvc.Domain.Entities.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId");

                    b.Navigation("Company");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.ProfileCustomer", b =>
                {
                    b.HasOne("OrderSvc.Domain.Entities.Company", "Company")
                        .WithMany("Customer")
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.VoucherProductCustomer", b =>
                {
                    b.HasOne("OrderSvc.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("OrderSvc.Domain.Entities.ProfileCustomer", "ProfileCustomer")
                        .WithMany()
                        .HasForeignKey("ProfileCustomerId");

                    b.HasOne("OrderSvc.Domain.Entities.Voucher", "Voucher")
                        .WithOne("VoucherProductCustomer")
                        .HasForeignKey("OrderSvc.Domain.Entities.VoucherProductCustomer", "VoucherId");

                    b.Navigation("Product");

                    b.Navigation("ProfileCustomer");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Jhipster.Domain.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Jhipster.Domain.User", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.Company", b =>
                {
                    b.Navigation("Customer");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.Product", b =>
                {
                    b.Navigation("CategoryProduct");
                });

            modelBuilder.Entity("OrderSvc.Domain.Entities.Voucher", b =>
                {
                    b.Navigation("VoucherProductCustomer");
                });
#pragma warning restore 612, 618
        }
    }
}
