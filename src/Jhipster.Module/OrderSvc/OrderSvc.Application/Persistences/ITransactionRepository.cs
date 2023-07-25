using Jhipster.Service.Utilities;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OrderSvc.Application.Persistences
{
    public interface ITransactionRepository
    {
        Task<Transactions> AddTransaction(Transactions transaction);
        Task<int> DeleteTransaction(Guid? Id);
        Task<int> UpdateTransaction(Transactions transaction);
        Task<Transactions> GetTransaction(Guid? Id);
        Task<PagedList<Transactions>> Search(string? type, int page , int pageSize);

    }
}
