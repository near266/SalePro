using AutoMapper;
using InteractiveSvc.Infrastructure.Persistences;
using Jhipster.Service.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Infrastructure.Persistences.Repositories
{
    public class PackgeMemberRepository :IPackageMember
    {
        private readonly OrderSvcDbContext _Db;
        private readonly IMapper _mapper;
        public PackgeMemberRepository(OrderSvcDbContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
        }

        public async Task<int> AddCus(ProfileCustomer customer)
        {
         await _Db.profileCustomer.AddAsync(customer);
          return  await _Db.SaveChangesAsync();
          
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

        public async Task<ProfileCustomer> GetDetailCus(Guid? Id)
        {
            var obj = await _Db.profileCustomer.Include(i=>i.Company).FirstOrDefaultAsync(i => i.Id == Id);
            if (obj == null) { return new ProfileCustomer(); }
            return obj;
        }

        public  async Task<PagedList<PackageMember>> SearchOrDetail(Guid? Id, string name, int page, int pageSize)
        {
            var query = _Db.packageMembers.AsQueryable();
            if (Id != null)
            {
                query = query.Where(i => i.Id.Equals(Id));
            }
            if (name != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.PackageName) && i.PackageName.ToLower().Contains(name.ToLower().Trim()));

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

        public async Task<PagedList<ProfileCustomer>> SearchOrDetail(string? name, int page, int pageSize)
        {
            var query = _Db.profileCustomer.AsQueryable();

            if (name != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.CustomerName) && i.CustomerName.ToLower().Contains(name.ToLower().Trim()));

            }

            var sQuery = query;
            var sQuery1 = await sQuery.Skip(pageSize * (page - 1))
                                .Take(pageSize)
                                .ToListAsync();
            var reslist = await sQuery.ToListAsync();
            return new PagedList<ProfileCustomer>
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
                _mapper.Map(obj,customer);
                return await _Db.SaveChangesAsync();
            }
            return 0;
        }

        public  async Task<int> UpdatePackge(PackageMember packageMember)
        {
            var obj = await _Db.categories.FindAsync(packageMember.Id);
            if (obj != null)
            {
                _mapper.Map(obj, packageMember);
                return await _Db.SaveChangesAsync();
            }
            return 0;
        }
    }

}
