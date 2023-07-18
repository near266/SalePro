using AutoMapper;
using InteractiveSvc.Infrastructure.Persistences;
using Jhipster.Service.Utilities;
using Microsoft.EntityFrameworkCore;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Infrastructure.Persistences.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IMapper _mapper;
        private readonly OrderSvcDbContext _Db;
        public TransactionRepository(IMapper mapper, OrderSvcDbContext db)
        {
            _mapper = mapper;
            _Db = db;
        }

        public async Task<int> AddTransaction(Transactions transaction)
        {
            await _Db.AddAsync(transaction);
            return await _Db.SaveChangesAsync();
        }

        public async Task<int> DeleteTransaction(Guid? Id)
        {
            var check = await _Db.Transactions.FirstOrDefaultAsync(i=>i.TransactionId.Equals(Id));
            if (check == null) { throw  new ArgumentException("not found"); }
            _Db.Transactions.Remove(check);
            return await _Db.SaveChangesAsync();    
        }

        public async Task<Transactions> GetTransaction(Guid? Id)
        {
           var obj = await _Db.Transactions.FirstOrDefaultAsync(i=>i.TransactionId.Equals(Id));
            if(obj == null) { throw new ArgumentException("not found"); }
            return obj;
        }

        public async Task<PagedList<Transactions>> Search(string? type, int page, int pageSize)
        {
            var query = _Db.Transactions.AsQueryable();
     
            if (type != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.TransactionType) && i.TransactionType.ToLower().Contains(type.ToLower().Trim()));

            }

            var sQuery = query;
            var sQuery1 = await sQuery.Skip(pageSize * (page - 1))
                                .Take(pageSize)
                                .ToListAsync();
            var reslist = await sQuery.ToListAsync();
            return new PagedList<Transactions>
            {
                Data = sQuery1,
                TotalCount = reslist.Count,
            };
        }

        public async Task<int> UpdateTransaction(Transactions transaction)
        {
            var obj = await _Db.Transactions.FirstOrDefaultAsync(i=>i.TransactionId.Equals(transaction.TransactionId));
            if(obj == null)
            {
                throw new ArgumentException("not found");
            }
              _mapper.Map(transaction, obj);
            return await _Db.SaveChangesAsync();
        }
    }
}
