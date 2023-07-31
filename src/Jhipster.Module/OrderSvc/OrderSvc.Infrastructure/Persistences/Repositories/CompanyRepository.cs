using AutoMapper;
using InteractiveSvc.Infrastructure.Persistences;
using Jhipster.Service.Utilities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Infrastructure.Persistences.Repositories
{
    public class CompanyRepository :ICompanyReqository
    {
        private readonly OrderSvcDbContext _Db;
        private readonly IMapper _mapper;
    public CompanyRepository(OrderSvcDbContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
        }

        public async Task<int> Add(Company company)
        {
            company.Id=Guid.NewGuid();
          await _Db.companies.AddAsync(company);
            return await _Db.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var check = await _Db.companies.FirstOrDefaultAsync(i=>i.Id.Equals(id));
            if(check == null) { throw new ArgumentException("not found"); }
             _Db.companies.Remove(check);
            return await _Db.SaveChangesAsync();
        }

        public async Task<Company> GetDetail(Guid id)
        {
            var obj = await _Db.companies.FirstOrDefaultAsync(i=>i.Id==id);
            if(obj == null) { return new Company(); }
            return obj;
        }

        public async Task<PagedList<Company>> Search(string? name, int page, int pageSize)
        {
            var query = _Db.companies.AsQueryable();
            if (name != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.CompanyName) && i.CompanyName.ToLower().Contains(name.ToLower().Trim()));

            }
            var sQuery = query.Include(i=>i.Products);
            var sQuery1 = await sQuery.Skip(pageSize * (page - 1))
                                .Take(pageSize)
                                .ToListAsync();
            var reslist = await sQuery.ToListAsync();
            return new PagedList<Company>
            {
                Data = sQuery1,
                TotalCount = reslist.Count,
            };
        }

        public async Task<int> Update(Company company)
        {
            var check = await _Db.companies.FirstOrDefaultAsync(i => i.Id.Equals(company.Id));

            if (check == null) { throw new ArgumentException("not found"); }
            else
            {
                _mapper.Map(company, check);
                return await _Db.SaveChangesAsync();
            }
        }
    }
}
