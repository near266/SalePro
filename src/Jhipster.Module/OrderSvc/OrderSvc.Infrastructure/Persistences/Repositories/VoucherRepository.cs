﻿ using AutoMapper;
using InteractiveSvc.Infrastructure.Persistences;
using Jhipster.Service.Utilities;
using MediatR;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Infrastructure.Persistences.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly OrderSvcDbContext _Db;
        private readonly IMapper _mapper;
  public  VoucherRepository(OrderSvcDbContext db, IMapper mapper)
        {
            _Db = db;
            _mapper= mapper;
        }

        public async Task<int> AddVoucher(Voucher voucher)
        {
         await _Db.vouchers.AddAsync(voucher);
            return await _Db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Voucher>> CheckVoucherExp()
        {
            var check = DateTime.Now;
            var qr =  await _Db.vouchers.Where(i=>i.EndDate< check).ToListAsync();

            return qr;
        }

        //public async Task<Voucher> CheckVoucherId(Guid Id)
        //{
        //    await 
        //}

        public async Task<int> DeleteVoucher(Guid Id)
        {
          var obj = await _Db.vouchers.FirstOrDefaultAsync(i=>i.Id == Id);
            if(obj ==null)
            {
                throw new ArgumentException("not found");
            }
            _Db.vouchers.Remove(obj);
            return await _Db.SaveChangesAsync();
        }

		public async Task<Voucher> GetDetail(Guid Id)
		{
			var obj = await _Db.vouchers.FirstOrDefaultAsync(i => i.Id.Equals(Id));
            if(obj ==null) { throw new ArgumentException("not found"); }
            return obj;
		}

        public async Task<PagedList<Voucher>> SearchVoucher(string? CodeVoucher, int page, int pageSize)
        {
            var query = _Db.vouchers.AsQueryable();
            if (CodeVoucher != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.CodeVoucher) && i.CodeVoucher.ToLower().Contains(CodeVoucher.ToLower().Trim()));

            }

            var sQuery = query;
            var sQuery1 = await sQuery.Skip(pageSize * (page - 1))
                                .Take(pageSize)
                                .ToListAsync();
            var reslist = await sQuery.ToListAsync();
            return new PagedList<Voucher>
            {
                Data = sQuery1,
                TotalCount = reslist.Count,
                Page = page,
                PageSize= pageSize
            };
        }

		public async Task<int> UpdateVoucher(Voucher voucher)
        {
            var obj = await _Db.vouchers.FindAsync(voucher.Id);
            var temp = new List<string>();
            temp.AddRange(obj.Image);

            if (obj != null)
            {
                if(voucher.Image.Count == 0)
                {
                    obj.Image.Clear();
                    voucher.Image = temp;
                }
                _mapper.Map(voucher, obj);
                return await _Db.SaveChangesAsync();
            }
            return 0;
        }
    }
}
