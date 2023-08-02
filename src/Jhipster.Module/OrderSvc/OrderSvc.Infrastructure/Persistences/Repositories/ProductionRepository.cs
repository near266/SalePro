using AutoMapper;
using Google.Apis.Upload;
using InteractiveSvc.Infrastructure.Persistences;
using Jhipster.Service.Utilities;
using MailKit.Net.Imap;
using Microsoft.EntityFrameworkCore;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Abstractions;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Infrastructure.Persistences.Repositories
{
    public class ProductionRepository :IProductRepository
    {
        private readonly OrderSvcDbContext _Db;
        private readonly IMapper _mapper;
        public ProductionRepository(OrderSvcDbContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
        }

        public async Task<int> Add(Product? product,CancellationToken cancellationToken)
        {  
        await _Db.products.AddAsync(product);
        return await _Db.SaveChangesAsync();

        }

        public async Task<int> Delete(Guid? id)
        {
            var obj = await _Db.products.FirstOrDefaultAsync(i=>i.Id.Equals(id));
            if (obj == null) { throw new ArgumentException("not found"); }
            _Db.products.Remove(obj);   
            return await _Db.SaveChangesAsync();
        }

        public async Task<ProductDTO> GetDetail(Guid id)
        {
            var obj = await _Db.products.Where(i=>i.Id.Equals(id)).Select(i => new ProductDTO
            {
                Id = i.Id,
                CategoryId = i.CategoryProduct.CategoryId,
                ProductName = i.ProductName,
                CategoryName = i.CategoryProduct.Category.CategoryName,
                Price = i.Price,
                PriceNum = i.PriceNum,
                warranty = i.warranty,
                Provider = i.Provider,
                CompanyId = i.CompanyId,
                CompanyName = i.Company.CompanyName,
                Image = i.Image,
                Decripstion = i.Decripstion,
                CategoryDescription = i.CategoryProduct.Category.CategoryDescription,



            }).FirstOrDefaultAsync();
            if (obj == null) {  throw new ArgumentException("not found"); }
            return obj;
        }

        public async Task <PagedList<ProductDTO>> SearchProduct(string? name, int page, int pageSize)
        {
            var query = _Db.products.AsQueryable();
            if (name != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.ProductName) && i.ProductName.ToLower().Contains(name.ToLower().Trim()));

            }
            var sQuery = query.Include(i => i.CategoryProduct).ThenInclude(i => i.Category).Select(i => new ProductDTO { 
             Id = i.Id,
             CategoryId=i.CategoryProduct.CategoryId,
             ProductName = i.ProductName,
             CategoryName=i.CategoryProduct.Category.CategoryName,
             Price = i.Price,
             PriceNum = i.PriceNum,
             warranty=i.warranty,
             Provider= i.Provider,
             CompanyId=i.CompanyId,
             CompanyName=i.Company.CompanyName,
             Image=i.Image,
             Decripstion=i.Decripstion,
             CategoryDescription=i.CategoryProduct.Category.CategoryDescription,


            
            });

            var sQuery1 = await sQuery.OrderBy(i=>i.ProductName).Skip(pageSize * (page - 1))
                                .Take(pageSize)
                                .ToListAsync();
            var reslist = await sQuery.ToListAsync();
            return new PagedList<ProductDTO>
            {
                Data = sQuery1,
                TotalCount = reslist.Count,
                Page=page,
                PageSize = pageSize,
            };

        }

        public async Task<int> Update(Product product)
        {
            var check = await _Db.products.FirstOrDefaultAsync(i=>i.Id.Equals(product.Id));
            List<string>? temp = new List<string>();
            temp.AddRange( check.Image);

            if (check == null) { throw new ArgumentException("not found"); }
            else
            {
             
                _mapper.Map(product, check);
                if (product.Image.Count == 0 || product.Image == null)
                {

                    check.Image.Clear();
                    check.Image.AddRange(temp);
                }

                return await _Db.SaveChangesAsync();
            }
        }
    }
}
