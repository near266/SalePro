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
    public class CategoryRepository :ICategoryRepository
    {
        private readonly OrderSvcDbContext _Db;
        private readonly IMapper _mapper;
        public CategoryRepository(OrderSvcDbContext db, IMapper mapper)
        {
                _Db = db;
            _mapper = mapper;   
        }

        public async Task<int> AddCate(Category category)
        {
            await _Db.categories.AddAsync(category);
            return  await _Db.SaveChangesAsync();
        }

        public async Task<int> DeleteCate(Guid id)
        {
            var check = await _Db.categories.FindAsync(id);
            if (check == null) { throw new ArgumentException("not foud"); }
            return await _Db.SaveChangesAsync();
        }

        public async Task<PagedList<Category>> SearchOrGetDetail(Guid? id, string? name, int page, int pageSize)
        {
             var query = _Db.categories.AsQueryable();
            if (name != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.CategoryName) && i.CategoryName.ToLower().Contains(name.ToLower().Trim()));

            }
            if(id != null)
            {
                query =query.Where(i=>i.Id.Equals(id));
            }
            var sQuery = query;
            var sQuery1 = await sQuery.Skip(pageSize * (page - 1))
                                .Take(pageSize)
                                .ToListAsync();
            var reslist = await sQuery.ToListAsync();
            return new PagedList<Category>
            {
                Data = sQuery1,
                TotalCount = reslist.Count,
            };
        }

        public async Task<int> UpdateCate(Category category)
        {
            var obj = await _Db.categories.FindAsync(category.Id);
            if (obj != null)
            {
                _mapper.Map(obj, category);
                return await _Db.SaveChangesAsync();
            }
            return 0;
        }
    }
}
