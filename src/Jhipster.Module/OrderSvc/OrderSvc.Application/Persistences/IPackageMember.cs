using Jhipster.Service.Utilities;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Persistences
{
    public interface IPackageMember 
    {
        Task<int> AddPackge(PackageMember packageMember);
        Task<int> DeletePackge(Guid Id);
        Task<int>UpdatePackge(PackageMember packageMember);
        Task<PagedList<PackageMember>> SearchOrDetailPackge(int?status, int page , int pageSize);
       // Task<int> GetStatusPackge(Guid Id);

        //Member
        Task<ProfileCustomer> AddCus(ProfileCustomer customer);
        Task<int> UpdateCus(ProfileCustomer customer);
        Task<int> DeleteCus(Guid? Id);
        Task<PagedList<ProfileRes>> SearchOrDetail(string? name , int page , int pageSize);
        Task<userResponse> GetDetailCus(Guid? Id);   

        // InfoPackage
        Task<InfoPackageMember> AddInfo(InfoPackageMember packageMember);
        Task<int> UpdateInfo(InfoPackageMember packageMember);
        Task<int> DeleteInfo(Guid? Id); 
        Task<PackageDto> GetInfoPackageMember(Guid? Id);
        Task<PagedList<PackageDto>> SearchPackageInfo(int? status, int page, int pageSize);

        Task<PackageDto> GetCurrentStatusByIdUser(Guid userId);
    }
}
