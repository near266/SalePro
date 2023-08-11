using AutoMapper;
using InteractiveSvc.Infrastructure.Persistences;
using Jhipster.Service.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrderSvc.Infrastructure.Persistences.Repositories
{

    public class PackgeMemberRepository :IPackageMember
    {
        private readonly OrderSvcDbContext _Db;
        private readonly IMapper _mapper;
        private readonly ILogger<PackgeMemberRepository> _logger;

        public PackgeMemberRepository(OrderSvcDbContext db, IMapper mapper,ILogger<PackgeMemberRepository> logger)
        {
            _Db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProfileCustomer> AddCus(ProfileCustomer user)
        {
            var check = await _Db.profileCustomer.Where(m => m.Username.ToUpper() == user.Username.ToUpper()).FirstOrDefaultAsync();
            if (check != null) _logger.LogError("Already Existed");
            user.CustomerName = user.Username;
            user.memberShip = 0;
            user.Role = 0;
            user.Avatar = "https://cdn.eztek.net/TrueConnect/Images/AvatarDefault_638191530952395772_ORIGIN.png";
            user.coverImage = "https://cdn.eztek.net/TrueConnect/Images/default-cover_638241704781692527_ORIGIN.png";
            user.CreatedDate = DateTime.Now;
            user.LastModifiedDate = DateTime.Now;
            await _Db.profileCustomer.AddAsync(user);
            await _Db.SaveChangesAsync();
            return user;

        }

        public async Task<int> AddPackge(PackageMember packageMember)
        {
            await _Db.packageMembers.AddAsync(packageMember);
            return await _Db.SaveChangesAsync();
        }

        public async Task<int> DeleteCus(Guid? Id)
        {
            var obj = await _Db.profileCustomer.FirstOrDefaultAsync(i => i.Id == Id);
            if (obj == null)
            {
                throw new ArgumentException("not found");
            }
            _Db.profileCustomer.Remove(obj);
            return await _Db.SaveChangesAsync();
        }

        public async Task<int> DeletePackge(Guid Id)
        {
            var obj = await _Db.packageMembers.FirstOrDefaultAsync(i=>i.Id.Equals(Id));
            if (obj == null) { throw new ArgumentException("not found"); }
            _Db.Remove(obj);
            return await _Db.SaveChangesAsync();
        }

        public async Task<userResponse> GetDetailCus(Guid? Id)
        {
            var obj = await _Db.profileCustomer.Where(i=>i.Id.Equals(Id)).Select(i=> new userResponse()
            {
                profileCustomer = _Db.profileCustomer.Where(i => i.Id.Equals(Id)).SingleOrDefault(),
                CompanyName = _Db.companies.Where(a => a.Id.Equals(i.CompanyId)).Select(a => a.CompanyName).FirstOrDefault(),
            }).FirstOrDefaultAsync();
            if (obj == null) { throw new ArgumentNullException("not found"); }
            return obj;
        }

        public async Task<PagedList<PackageMember>> SearchOrDetailPackge(int? status, int page, int pageSize)
        {

            var query = _Db.packageMembers.AsQueryable();

            if (status != null)
            {
                query = query.Where(i =>i.StatusPackage.Equals(status));

            }


            var sQuery = query;
            var sQuery1 = await sQuery.Skip(pageSize * (page - 1))
                                .Take(pageSize)
                                .ToListAsync();
            var reslist = await sQuery.ToListAsync();
            return new PagedList<PackageMember>
            {
                Data = sQuery1,
                TotalCount = reslist.Count,
            };
        }

        public async Task<PagedList<ProfileRes>> SearchOrDetail( string? name, int page, int pageSize)
        {
            var query = _Db.profileCustomer.AsQueryable();

            if (name != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.CustomerName) && i.CustomerName.ToLower().Contains(name.ToLower().Trim()));

            }
         

            var sQuery = query;
            var sQuery1 =  sQuery
                .Select( i=>new ProfileRes()
                {
                    Id = i.Id,
                   CustomerName= i.CustomerName,
                   Username= i.Username,
                   CompanyId= i.CompanyId,
                   Status=i.Status,
                   DOB= i.DOB,
                   Position= i.Position,
                   Decripstion= i.Decripstion,
                   Avatar= i.Avatar,
                   coverImage=i.coverImage,
                   Email=i.Email,
                   PhoneNumber=i.PhoneNumber,
                   Bio=i.Bio,
                   Role=i.Role,
                   memberShip=i.memberShip,
                  
                   
                    CompanyName= _Db.companies.Where(a=>a.Id.Equals(i.CompanyId)).Select(a=>a.CompanyName).FirstOrDefault(),
                })
                .Skip(pageSize * (page - 1))

                                .Take(pageSize)
                                .ToList();
            var reslist = await sQuery.ToListAsync();
            return new PagedList<ProfileRes>
            {
                Data = sQuery1,
                TotalCount = reslist.Count,
                Page = page,
                PageSize = pageSize
                
            };
        }

        public async Task<int> UpdateCus(ProfileCustomer customer)
        {
            var obj = await _Db.profileCustomer.FirstOrDefaultAsync(i=>i.Id.Equals(customer.Id));
            if (obj != null)
            {
                _mapper.Map(customer, obj);
                customer.LastModifiedDate = DateTime.UtcNow;
               return await _Db.SaveChangesAsync();
               
            }
            return -1;
        }

        public  async Task<int> UpdatePackge(PackageMember packageMember)
        {
            var obj = await _Db.categories.FindAsync(packageMember.Id);
            if (obj != null)
            {
                _mapper.Map(packageMember, obj);
                return await _Db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<InfoPackageMember> AddInfo(InfoPackageMember packageMember)
        {
             await _Db.infoPackages.AddAsync(packageMember);
            await _Db.SaveChangesAsync();
            return packageMember;
        }

        public async Task<int> UpdateInfo(InfoPackageMember packageMember)
        {
            var obj = await _Db.infoPackages.FirstOrDefaultAsync(i => i.Id.Equals(packageMember.Id));
            if(obj == null)
            {
                throw new ArgumentException("not found");

            }
            _mapper.Map(packageMember, obj);
            return await _Db.SaveChangesAsync();
        }

        public async Task<int> DeleteInfo(Guid? Id)
        {
           var obj = _Db.infoPackages.FirstOrDefault(i => i.Id == Id);
            if(obj == null)
            {
                throw new ArgumentException("not found");

            }
            _Db.Remove(obj);
            return await _Db.SaveChangesAsync();
        }

        public async Task<PackageDto> GetInfoPackageMember(Guid? Id)
        {
            var obj =  await _Db.infoPackages.Where(i=>i.Id.Equals(Id)).Include(i=>i.ProfileCustomer).Select(i=>new PackageDto
            {
                UserId=i.ProfileMemberId,
                Avatar = _Db.profileCustomer.Where(a => a.Id.Equals(i.ProfileMemberId)).Select(a => a.Avatar).FirstOrDefault(),
                StatusCus = i.ProfileCustomer.Status,
                CurrentStatus = i.CurrentStatusMember,
                UserName = _Db.profileCustomer.Where(a=>a.Id.Equals(i.ProfileMemberId)).Select(a=>a.Username).FirstOrDefault(),
                CustomerName= _Db.profileCustomer.Where(a=>a.Id.Equals(i.ProfileMemberId)).Select(a=>a.CustomerName).FirstOrDefault(),
                status=i.status,
                


            }).FirstOrDefaultAsync();
            if(obj == null)
            {
                throw new ArgumentException("not found");

            }
            
            return obj;

        }
         


        public async Task<PagedList<PackageDto>> SearchPackageInfo(List<int>? status, int page, int pageSize)
        {
            var query = _Db.infoPackages.OrderBy(i=>i.status).AsQueryable();

            if (status != null)
            {
                query = query.Where(i=>status.Contains((int) i.status));

            }


            var sQuery = query;
            var sQuery1 = sQuery
                .Select(i => new PackageDto()
                {
                    UserId = i.ProfileMemberId,
                    Avatar=_Db.profileCustomer.Where(a=>a.Id.Equals(i.ProfileMemberId)).Select(a=>a.Avatar).FirstOrDefault(),
                    CurrentStatus = i.CurrentStatusMember,
                    StatusCus = i.ProfileCustomer.Status,
                    UserName = _Db.profileCustomer.Where(a => a.Id.Equals(i.ProfileMemberId)).Select(a => a.Username).FirstOrDefault(),
                    CustomerName = _Db.profileCustomer.Where(a => a.Id.Equals(i.ProfileMemberId)).Select(a => a.CustomerName).FirstOrDefault(),
                    status = i.status,
                })
                .Skip(pageSize * (page - 1))

                                .Take(pageSize)
                                .ToList();
            var reslist = await sQuery.ToListAsync();
            return new PagedList<PackageDto>
            {
                Data = sQuery1,
                TotalCount = reslist.Count,
                Page = page,
                PageSize = pageSize

            };
        }

        public async Task<PackageDto> GetCurrentStatusByIdUser(Guid userId)
        {
            var obj = await _Db.infoPackages.Where(i => i.ProfileMemberId.Equals(userId)).Select(i=>new PackageDto()
            {
                Id=i.Id,
                UserId = i.ProfileMemberId,
                Avatar = _Db.profileCustomer.Where(a => a.Id.Equals(i.ProfileMemberId)).Select(a => a.Avatar).FirstOrDefault(),
                CurrentStatus = i.CurrentStatusMember,
                StatusCus = i.ProfileCustomer.Status,
                UserName = _Db.profileCustomer.Where(a => a.Id.Equals(i.ProfileMemberId)).Select(a => a.Username).FirstOrDefault(),
                CustomerName = _Db.profileCustomer.Where(a => a.Id.Equals(i.ProfileMemberId)).Select(a => a.CustomerName).FirstOrDefault(),
                status = i.status,

            }).FirstOrDefaultAsync();
            if(obj == null) { throw new ArgumentException("ProfileMemberId is not exits"); }
            return obj;

        }

        public  async Task<TotalResponse> GetTotal()
        {
            var TotalOrder = _Db.orders.Count();
            var TotalPtoduct = _Db.products.Count();
            var TotalRequestUpgrade = _Db.infoPackages.Where(i => i.status == 1 || i.status == 2).Count();
            var TotalVoucher = _Db.vouchers.Count();
            var res = new TotalResponse
            { 
                TotalOrder= TotalOrder,
                TotalPtoduct= TotalPtoduct,
                TotalRequestUpgrade= TotalRequestUpgrade,
                TotalVoucher= TotalVoucher
               
            };
            if (res == null) { throw new ArgumentException("not found"); }
           
            return res;
        }

        //public async Task<int> GetStatusPackge(Guid Id)
        //{
        //    var obj = await _Db.packageMembers.FirstOrDefaultAsync(i => i.Id == Id);
        //    if(obj == null)
        //    {
        //        return -1;
        //    }
        //    return (int) obj.Status;
        //}
    }

}
