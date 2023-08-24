using AutoMapper;
using Jhipster.Service.Utilities;
using OrderSvc.Application.Command.CategoryCommand;
using OrderSvc.Application.Command.CompanyCommand;
using OrderSvc.Application.Command.OrderCommand;
//using OrderSvc.Application.Command.OrderCommand;
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
            CreateMap<Product, UpdateProductCommand>()
                .ReverseMap()
                .ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null)); ;
            CreateMap<Product, UpdateProductCommand>()
           .ForMember(dest => dest.Image, opt => opt.MapFrom((src, dest) => src.Image ?? dest.Image));
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
            CreateMap<ProfileCustomer, UpdateCustomerCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<ProfileRes, ProfileCustomer>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap(typeof(PagedList<ProfileRes>), typeof(PagedList<ProfileCustomer>));
            CreateMap<PagedList<ProfileRes>, PagedList<ProfileCustomer>>().ReverseMap();
            //Package
            CreateMap<PackageMember, PackageMember>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<CreatePackageCommand, PackageMember>();

			//Transactions
			CreateMap<Transactions, Transactions>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			CreateMap<Transactions, AddTransactionsCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			CreateMap<Transactions, UpdateTransactionsCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            //Order

            CreateMap<Order, Order>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
          
           CreateMap<Order, UpdateStatusOrderCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));

          CreateMap<Order, CreateOrderCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            //Affiliate
            CreateMap<Affiliates, Affiliates>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
              CreateMap<Affiliates, AddAffiliateCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<Affiliates, UpdateAffiliateC>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));

            // OrderItems
            CreateMap<OrderItem, OrderItem>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
          
            CreateMap<OrderItem, CreateOrderItemC>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<OrderItem, UpdateOrderItemC>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            // InfoPackage
            CreateMap<InfoPackageMember, InfoPackageMember>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<InfoPackageMember, AddInfoPackageC>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<InfoPackageMember, PackageDto>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<InfoPackageMember, UpdateInfoPackageC>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));


        }
    }
}
