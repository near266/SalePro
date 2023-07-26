using AutoMapper;
using Jhipster.Service.Utilities;
using OrderSvc.Application.Command.CategoryCommand;
using OrderSvc.Application.Command.CompanyCommand;
using OrderSvc.Application.Command.OrderCommand;
using OrderSvc.Application.Command.PackageCommand;
using OrderSvc.Application.Command.ProductCommand;
using OrderSvc.Application.Command.TransactionsCommand;
using OrderSvc.Application.Command.VoucherCommand;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System.ComponentModel;

namespace OrderSvc.Application.Configurations.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Product
            CreateMap<Product, Product>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<AddProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
            CreateMap(typeof(PagedList<Product>), typeof(PagedList<ProductDTO>));
            CreateMap<Product, ProductDTO>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null)); 
           
            CreateMap<Product, ProductDTO>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null)); 
            //Category
            CreateMap<Category, Category>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<CategoryProduct, CategoryProduct>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<CreateCategoryProductCommand, CategoryProduct>();
            

            // Company
            CreateMap<Company, Company>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<CreateCompanyCommand,Company>();
            CreateMap<UpdateCompanyCommand, Company>();
            // Voucher
            CreateMap<Voucher, Voucher>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			CreateMap<Voucher, AddVoucherCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			CreateMap<Voucher, UpdateVoucherCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			// Profile
            CreateMap<ProfileCustomer, ProfileCustomer>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			CreateMap<ProfileCustomer, AddProfileCustomerCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));

            //Package
            CreateMap<PackageMember, PackageMember>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<CreatePackageCommand, PackageMember>();

			//Transactions
			CreateMap<Transactions, Transactions>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			CreateMap<Transactions, AddTransactionsCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			CreateMap<Transactions, UpdateTransactionsCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            //Order

            CreateMap<Order, Order>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<Order, CreateOrderCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            //Affiliate
            CreateMap<Affiliates, Affiliates>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<Affiliates, AddAffiliateCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));


        }
    }
}
