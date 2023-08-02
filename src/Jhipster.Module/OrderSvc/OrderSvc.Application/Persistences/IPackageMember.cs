using Jhipster.Service.Utilities;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
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
        //Task<PagedList<PackageMember>> SearchOrDetail(Guid? Id,string name , int page , int pageSize);

        Task<ProfileCustomer> AddCus(ProfileCustomer customer);
        Task<int> UpdateCus(ProfileCustomer customer);
        Task<int> DeleteCus(Guid? Id);
        Task<PagedList<userResponse>> SearchOrDetail(string? name , int page , int pageSize);
        Task<userResponse> GetDetailCus(Guid? Id);   

    }
}
