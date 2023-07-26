using Jhipster.Service.Utilities;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Persistences
{
    public interface IPackageMember 
    {
        Task<int> AddPackge(PackageMember packageMember);
        Task<int> DeletePackge(Guid Id);
        Task<int>UpdatePackge(PackageMember packageMember);
        Task<PagedList<PackageMember>> SearchOrDetail(Guid? Id,string name , int page , int pageSize);

        Task<int> AddCus(ProfileCustomer customer);
        Task<ProfileCustomer> UpdateCus(ProfileCustomer customer);
        Task<int> DeleteCus(Guid? Id);
        Task<ProfileCustomer> GetDetailCus(Guid? Id);   

    }
}
