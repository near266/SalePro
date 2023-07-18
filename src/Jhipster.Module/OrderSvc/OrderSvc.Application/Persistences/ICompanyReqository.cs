using Jhipster.Service.Utilities;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Persistences
{
    public interface ICompanyReqository
    {
        Task<int> Add(Company company);
        Task<int> Update(Company company);
        Task<int> Delete(Guid id);
        Task<Company> GetDetail(Guid id);
        Task<PagedList< Company>> Search(string? name,int page, int pageSize);
    }
}
