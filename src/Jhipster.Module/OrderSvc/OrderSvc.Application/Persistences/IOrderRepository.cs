using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Persistences
{
    public interface IOrderRepository
    {
      Task<int> AddOrder(Order order);
      Task<int> UpdateOrder(Order order ,CancellationToken cancellationToken);
      Task<int> DeleteOrder(Guid? id);
      Task<OrderResponse> GetOrderById(Guid id);
       Task<int> AddAffi(Affiliates affiliates);

        Task<PriceDto> Price(List<Guid>? ProductId, Guid? VoucherId);
    }
}
