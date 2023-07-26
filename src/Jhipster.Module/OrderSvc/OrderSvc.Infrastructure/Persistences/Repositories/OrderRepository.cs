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

        public Task<Order> GetOrderById(Guid id)
        {
            throw new NotImplementedException();
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
          
            voucher = v != null ? total - total *(double) v.Discount : 0;

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
