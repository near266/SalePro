using AutoMapper;
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


            // Company
            CreateMap<Company, Company>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            // Voucher
            CreateMap<Voucher, Voucher>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            // Profile
            CreateMap<ProfileCustomer, ProfileCustomer>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            //Package
            CreateMap<PackageMember, PackageMember>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));

        }
    }
}
