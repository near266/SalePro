using AutoMapper;
using InteractiveSvc.Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Infrastructure.Persistences.Repositories
{
     public class OrderRepository :IOrderRepository
    {
        private readonly OrderSvcDbContext _Db;
        private readonly IMapper _mapper;
        public OrderRepository(OrderSvcDbContext db, IMapper mapper)
        {
            _Db = db;   
            _mapper=mapper;
        }
        // Affi
        public async Task<int> AddAffi(Affiliates affiliates)
        {
           await _Db.affiliates.AddAsync(affiliates);
            return await _Db.SaveChangesAsync();
        }

        public async Task<int> AddOrder(Order order)
        {
            await _Db.orders.AddAsync(order);
            return await _Db.SaveChangesAsync();
        }

        public Task<int> DeleteOrder(Guid? id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderResponse> GetOrderById(Guid id)
        {
            var trsQr = _Db.Transactions.AsQueryable();
            var User = _Db.profileCustomer.AsQueryable();
            var pro =await _Db.products.Where(i=>i.OrderId==id).Select(i=>new product
            {
                Id=i.Id,
                ProductName=i.ProductName,
                price=i.Price,
                CateId=i.CategoryProduct.CategoryId,
                CateName= _Db.categories.Where(a=>a.Id.Equals(i.CategoryProduct.Id)).Select(a=>a.CategoryName).FirstOrDefault(),
            }).ToListAsync();
            var order = _Db.orders.Where(i => i.Id == id).AsQueryable();
            var check = order.FirstOrDefault();
            var AffiQr=_Db.affiliates.Where(a => a.Id.Equals(check.AffiliateId)).FirstOrDefault();
            var prod = new ProductDTOs { 
                quantity=pro.Count(),
                products=pro,
            };
            List<Guid>? listProId= pro.Select(a=>(Guid) a.Id).ToList();
            var v = order.Select(i=>i.VoucherId).SingleOrDefault();
            var fee = Price(listProId,v);
            
            var payment = new Payment { 
                total=fee.Result.TotalPrice,
                discount=fee.Result.PriceVoucher,
                finalPrice=fee.Result.RealPice,
            };

            var affi = order.Select(i => new AffiliateDTO
            {
                SalerId=AffiQr.SalerId,
                SalerName=User.Where(a=>a.Id.Equals(AffiQr.SalerId)).Select(a=>a.CustomerName).FirstOrDefault(),
                ProviderId=AffiQr.ProviderId,
                ProviderName= User.Where(a => a.Id.Equals(AffiQr.ProviderId)).Select(a => a.CustomerName).FirstOrDefault(),
                ParticipantsId=AffiQr.ParticipantsId,
                ParticipantsName= User.Where(a => a.Id.Equals(AffiQr.ParticipantsId)).Select(a => a.CustomerName).FirstOrDefault(),
                ReferrerId=AffiQr.ReferrerId,
                ReferrerName= User.Where(a => a.Id.Equals(AffiQr.ReferrerId)).Select(a => a.CustomerName).FirstOrDefault(),

            }).SingleOrDefault();

            var obj = order
               .Select(i => new OrderResponse
                {
                    Id= i.Id,
                    TransactionId= i.TransactionId,
                    TransactionDate=trsQr.Where(a=>a.TransactionId.Equals(i.TransactionId)).Select(a=>a.TransactionDate).FirstOrDefault(),
                    TransactionName= trsQr.Where(a => a.TransactionId.Equals(i.TransactionId)).Select(a => a.TransactionName).FirstOrDefault(),
                    SalePerson =i.SalePerson,
                    SalerName =User.Where(a=>a.Id.Equals(i.SalePerson)).Select(a=>a.CustomerName).FirstOrDefault(),
                    BoughtPerson=i.BoughtPerson,
                    BoughtPersonName= User.Where(a => a.Id.Equals(i.BoughtPerson)).Select(a => a.CustomerName).FirstOrDefault(),
                    TotalAmount= trsQr.Where(a => a.TransactionId.Equals(i.TransactionId)).Select(a => a.TotalAmount).FirstOrDefault(),
                    PaymentMethod= trsQr.Where(a => a.TransactionId.Equals(i.TransactionId)).Select(a => a.PaymentMethod).FirstOrDefault(),
                    TransactionType= trsQr.Where(a => a.TransactionId.Equals(i.TransactionId)).Select(a =>a.TransactionType).FirstOrDefault(),
                    affiliate=affi,
                    product=prod,
                    payment=payment

                })
                .AsNoTracking();

            return obj.SingleOrDefault(); 
        }

        public async Task<PriceDto> Price(List<Guid>? ProductId, Guid? VoucherId)
        {
            var res = new PriceDto();
            double? total = 0;
            double? voucher = 0;
            double? price = 0;
            var pro = await _Db.products.Where(i => ProductId.Contains(i.Id)).ToListAsync();
            var v = await _Db.vouchers.Where(i=>i.Id==VoucherId).FirstOrDefaultAsync();
            foreach (var item in pro)
            {
                if (item.Price != null)
                {
                total+= item.Price;
                }
            }
          
            voucher = v != null ? total *((double) v.Discount /100): 0;

            price = total - voucher;
            return new PriceDto { 
                TotalPrice=total,
                PriceVoucher=voucher,
                RealPice=price,
            };

        }

        public Task<int> UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
