using AutoMapper;
using InteractiveSvc.Infrastructure.Persistences;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
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

        public Task<int> UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
