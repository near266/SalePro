using Jhipster.Service.Utilities;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Persistences
{
    public interface IVoucherRepository
    {
        Task<int> AddVoucher(Voucher voucher);
        Task<int> UpdateVoucher(Voucher voucher);
        Task<int> DeleteVoucher(Guid Id);
		Task<Voucher> GetDetail(Guid Id);
        Task<IEnumerable <Voucher>> CheckVoucherExp();
		Task<PagedList<Voucher>> SearchVoucher(string? CodeVoucher, int page ,int pageSize);
         
    }
}
