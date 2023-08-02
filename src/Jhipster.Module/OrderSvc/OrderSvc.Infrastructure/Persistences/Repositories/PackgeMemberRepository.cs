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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                products = _Db.products.Where(a => a.CompanyId.Equals(i.CompanyId)).Select(a => a.ProductName).ToList(),
                CompanyName = _Db.companies.Where(a => a.Id.Equals(i.CompanyId)).Select(a => a.CompanyName).FirstOrDefault(),
            }).FirstOrDefaultAsync();
            if (obj == null) { throw new ArgumentNullException("not found"); }
            return obj;
        }

        //public  async Task<PagedList<PackageMember>> SearchOrDetail(Guid? Id, string name, int page, int pageSize)
        //{
        //    var query = _Db.packageMembers.AsQueryable();
        //    if (Id != null)
        //    {
        //        query = query.Where(i => i.Id.Equals(Id));
        //    }
        //    if (name != null)
        //    {
        //        query = query.Where(i => !string.IsNullOrEmpty(i.PackageName) && i.PackageName.ToLower().Contains(name.ToLower().Trim()));

        //    }
           
        //    var sQuery = query;
        //    var sQuery1 = await sQuery.Skip(pageSize * (page - 1))
        //                        .Take(pageSize)
        //                        .ToListAsync();
        //    var reslist = await sQuery.ToListAsync();
        //    return new PagedList<PackageMember>
        //    {
        //        Data = sQuery1,
        //        TotalCount = reslist.Count,
        //    };
        //}

        public async Task<PagedList<userResponse>> SearchOrDetail(string? name, int page, int pageSize)
        {
            var query = _Db.profileCustomer.AsQueryable();

            if (name != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.CustomerName) && i.CustomerName.ToLower().Contains(name.ToLower().Trim()));

            }

            var sQuery = query;
            var sQuery1 =  sQuery
                .Select( i=>new userResponse()
                {
                    profileCustomer= _Db.profileCustomer.Where(a=>a.Id.Equals(i.Id)).FirstOrDefault(),
                    products=_Db.products.Where(a=>a.CompanyId.Equals(i.CompanyId)).Select(a=>a.ProductName).ToList(),
                    CompanyName= _Db.companies.Where(a=>a.Id.Equals(i.CompanyId)).Select(a=>a.CompanyName).FirstOrDefault(),
                })
                .Skip(pageSize * (page - 1))

                                .Take(pageSize)
                                .ToList();
            var reslist = await sQuery.ToListAsync();
            return new PagedList<userResponse>
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
    }

}
