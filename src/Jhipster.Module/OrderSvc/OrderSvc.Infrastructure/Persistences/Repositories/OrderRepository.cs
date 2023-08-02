using AutoMapper;
using Google.Apis.Upload;
using InteractiveSvc.Infrastructure.Persistences;
using Jhipster.Infrastructure.Migrations;
using Jhipster.Service.Utilities;
using Microsoft.EntityFrameworkCore;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OrderSvc.Infrastructure.Persistences.Repositories
{

    public class OrderRepository : IOrderRepository
    {
        private readonly OrderSvcDbContext _Db;
        private readonly IMapper _mapper;
        public OrderRepository(OrderSvcDbContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
        }
     
        public async Task<int> AddAffi(Affiliates affiliates)
        {
            await _Db.affiliates.AddAsync(affiliates);
            return await _Db.SaveChangesAsync();
        }

        public async Task<int> AddOrder(Order order)
        {
            await _Db.orders.AddAsync(order);
            return await _Db.SaveChangesAsync();
        }

        public async Task<OrderItem> CreateOrderItem(OrderItem item)
        {
            await _Db.orderItems.AddAsync(item);
            await _Db.SaveChangesAsync();
            return item;
        }

        public Task<int> DeleteOrder(Guid? id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteOrderItem(Guid Id)
        {
            var check = await _Db.orderItems.Where(i=>i.Equals(Id)).FirstOrDefaultAsync();
            if (check == null)
            {
                throw new ArgumentException("not found");
            }
            _Db.orderItems.Remove(check);
             return await _Db.SaveChangesAsync();   
        }

        public async Task<OrderResponse> GetOrderById(Guid id)
        {
            var trsQr = _Db.Transactions.AsQueryable();
            var User = _Db.profileCustomer.AsQueryable();
            var pro = await _Db.orderItems.Where(i => i.OrderId == id).Select(i => new product
            {
                Id = i.ProductId,
                ProductName = i.Product.ProductName,
                Image = i.Product.Image,
                price = i.Product.Price,
                CateId = i.Product.CategoryProduct.CategoryId,
                CateName = i.Product.CategoryProduct.Category.CategoryName,
                Quantity=i.Quantity
            }).ToListAsync();
            var order = _Db.orders.Where(i => i.Id == id).AsQueryable();
            var check = order.FirstOrDefault();
            var AffiQr = _Db.affiliates.Where(a => a.Id.Equals(check.AffiliateId)).FirstOrDefault();
            var prod = new ProductDTOs() { products=pro };



            List<Guid>? listProId = pro.Select(a => (Guid)a.Id).ToList();
            var v = order.Select(i => i.VoucherId).SingleOrDefault();
            //var fee = Price(listProId,v);

            //var payment = new Payment
            //{
            //    total = fee.Result.TotalPrice,
            //    discount = fee.Result.PriceVoucher,
            //    finalPrice = fee.Result.RealPice,
            //};
            var fee = PriceId(id);

            var payment = new Payment
            {
                total = fee.Result.total,
                discount = fee.Result.discount,
                finalPrice = fee.Result.finalPrice,
            };

            var affi = order.Select(i => new AffiliateDTO
            {
                SalerId = AffiQr.SalerId,
                SalerName = User.Where(a => a.Id.Equals(AffiQr.SalerId)).Select(a => a.CustomerName).FirstOrDefault(),
                ProviderId = AffiQr.ProviderId,
                ProviderName = User.Where(a => a.Id.Equals(AffiQr.ProviderId)).Select(a => a.CustomerName).FirstOrDefault(),
                ParticipantsId = AffiQr.ParticipantsId,
                ParticipantsName = User.Where(a => a.Id.Equals(AffiQr.ParticipantsId)).Select(a => a.CustomerName).FirstOrDefault(),
                ReferrerId = AffiQr.ReferrerId,
                ReferrerName = User.Where(a => a.Id.Equals(AffiQr.ReferrerId)).Select(a => a.CustomerName).FirstOrDefault(),
                Ecosystem = AffiQr.Ecosystem,

            }).SingleOrDefault();

            var obj = order
               .Select(i => new OrderResponse
               {
                   Id = i.Id,
                   TransactionId = i.TransactionId,
                   TransactionDate = trsQr.Where(a => a.TransactionId.Equals(i.TransactionId)).Select(a => a.TransactionDate).FirstOrDefault(),
                   TransactionName = trsQr.Where(a => a.TransactionId.Equals(i.TransactionId)).Select(a => a.TransactionName).FirstOrDefault(),
                   SalePerson = i.SalePerson,
                   SalerName = User.Where(a => a.Id.Equals(i.SalePerson)).Select(a => a.CustomerName).FirstOrDefault(),
                   BoughtPerson = i.BoughtPerson,
                   BoughtPersonName = User.Where(a => a.Id.Equals(i.BoughtPerson)).Select(a => a.CustomerName).FirstOrDefault(),
                   TotalAmount = trsQr.Where(a => a.TransactionId.Equals(i.TransactionId)).Select(a => a.TotalAmount).FirstOrDefault(),
                   PaymentMethod = trsQr.Where(a => a.TransactionId.Equals(i.TransactionId)).Select(a => a.PaymentMethod).FirstOrDefault(),
                   TransactionType = trsQr.Where(a => a.TransactionId.Equals(i.TransactionId)).Select(a => a.TransactionType).FirstOrDefault(),
                   affiliate = affi,
                   product = prod,
                   payment = payment

               })
                .AsNoTracking();

            return obj.SingleOrDefault();
        }

        public async Task<PriceDto> Price(List<Guid>? ProductId, Guid? VoucherId)
        {
            var res = new PriceDto();
            double? total = 0;
            double? voucher = 0;
            double? price = 0;
            var pro = await _Db.orderItems.Where(i => ProductId.Contains(i.ProductId)).ToListAsync();
            var v = await _Db.vouchers.Where(i => i.Id == VoucherId).FirstOrDefaultAsync();
            foreach (var item in pro)
            {
              
                    total += _Db.products.Where(i=>i.Id.Equals(item.ProductId)).Select(i=>i.Price).FirstOrDefault()*item.Quantity;
                
            }

            voucher = v != null ? total * ((double)v.Discount / 100) : 0;

            price = total - voucher;
            return new PriceDto
            {
                TotalPrice = total,
                PriceVoucher = voucher,
                RealPice = price,
            };

        }

        public async Task<Payment> PriceId(Guid id)
        {

             var order = await _Db.orders.Where(i=>i.Id.Equals(id)).FirstOrDefaultAsync();
            var v = await _Db.vouchers.Where(i=>i.Id.Equals(order.VoucherId)).FirstOrDefaultAsync();
            var orderItem = _Db.orderItems.Where(i=>i.OrderId.Equals(id)).ToList();
            
            var res = new PriceDto();
            double? total = 0;
            double? voucher = 0;
            double? price = 0;
            foreach (var item in orderItem)
            {
                total += _Db.products.Where(i=>i.Id.Equals(item.ProductId)).Select(i=>i.Price).FirstOrDefault() * item.Quantity  ;
            }

            voucher = v != null ? total * ((double) v.Discount / 100) : 0;

            price = total - voucher;

            return new Payment
            {
                total = total,
                discount = voucher,
                finalPrice = price,
            };

        }


        public async Task<PagedList<SearchOrder>> SearchOrder(string? name, int? status, int page, int pageSize)
        {
            var query = _Db.orders.Include(i => i.OrderItem).ThenInclude(i=>i.Product).AsQueryable();
            if (name != null)
            {
                var queryPro = _Db.products
                    .Where(i => i.ProductName.ToLower().Trim().Equals(name.ToLower().Trim()))
                    .AsQueryable();
                var Idpro = queryPro.Select(i => i.Id).ToList();
                var orderId = await _Db.orderItems.Where(i => Idpro.Contains(i.ProductId)).ToListAsync();
                query = query.Where(i => orderId.Select(i=>i.OrderId).Contains(i.Id));
            }
            if (status != null)
            {
                query = query.Where(i => i.Status == status);
            }

            var result = await query.Select(i => new SearchOrder
            {
                Id = i.Id,
                TransactionId = i.TransactionId,
                ProductDTOs = new ProductDTOs
                {
                    products = _Db.orderItems.Where(a=>a.OrderId.Equals(i.Id)).Select(a => new product
                    {
                        Id = a.Id,
                        ProductName = a.Product.ProductName,
                        Image = a.Product.Image,
                        price = a.Product.Price,
                        CateId = a.Product.CategoryProduct.CategoryId,
                        CateName = a.Product.CategoryProduct.Category.CategoryName,
                    }).ToList(),

                },




            }).Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<SearchOrder>
            {
                Data = result,
                TotalCount = result.Count(),
                Page = page,
                PageSize = pageSize,
            };
        }

        public async Task<int> UpdateOrder(Order order, CancellationToken cancellationToken)
        {
            var obj = await _Db.orders.FirstOrDefaultAsync(i => i.Id.Equals(order.Id));
            if (obj != null)
            {
                obj.Status = order.Status;
                _mapper.Map<Order, Order>(obj, order);
                return await _Db.SaveChangesAsync(cancellationToken);
            }
            return 0;
        }

        public async Task<OrderItem> UpdateOrderItem(OrderItem item)
        {
            var old = await _Db.orderItems.FirstOrDefaultAsync(i=>i.Equals(item.Id));
            if (old==null)
            {
                throw new ArgumentException("not found");
            }
            _mapper.Map<OrderItem,OrderItem>(item, old);
            await _Db.SaveChangesAsync();
            return old; 
        }
    }
}
