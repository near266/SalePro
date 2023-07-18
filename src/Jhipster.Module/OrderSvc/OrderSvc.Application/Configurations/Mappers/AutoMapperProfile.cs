﻿using AutoMapper;
using OrderSvc.Application.Command.ProductCommand;
using OrderSvc.Application.Command.TransactionsCommand;
using OrderSvc.Application.Command.VoucherCommand;
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
			CreateMap<Voucher, AddVoucherCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			CreateMap<Voucher, UpdateVoucherCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			// Profile
			CreateMap<ProfileCustomer, ProfileCustomer>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            //Package
            CreateMap<PackageMember, PackageMember>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			//Transactions
			CreateMap<Transactions, Transactions>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			CreateMap<Transactions, AddTransactionsCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			CreateMap<Transactions, UpdateTransactionsCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));

		}
    }
}
