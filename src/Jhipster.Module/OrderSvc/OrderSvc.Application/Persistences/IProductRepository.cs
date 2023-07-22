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
    public interface IProductRepository
    {
        Task<int> Add(Product? product,CancellationToken cancellationToken);
        Task<int> Update(Product? product);
        Task<int> Delete(Guid? id);
        Task<ProductDTO> GetDetail(Guid id);
        Task<PagedList<ProductDTO>> SearchProduct(string? name, int page , int pageSize);
    }
}
