using AutoMapper;
using OrderSvc.Application.Command.CategoryCommand;
using OrderSvc.Application.Command.CompanyCommand;
using OrderSvc.Application.Command.ProductCommand;
using OrderSvc.Domain.Entities;

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
            // Profile
            CreateMap<ProfileCustomer, ProfileCustomer>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            //Package
            CreateMap<PackageMember, PackageMember>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));

        }
    }
}
