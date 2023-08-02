using AutoMapper;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.OrderCommand
{
    public class CreateOrderItemC :IRequest<OrderItem>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int Quantity { get; set; }

        
    }
    public class CreateOrderItemCHandler : IRequestHandler<CreateOrderItemC, OrderItem>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repo;
        public CreateOrderItemCHandler(IMapper mapper, IOrderRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<OrderItem> Handle(CreateOrderItemC rq, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<OrderItem>(rq);
         return  await _repo.CreateOrderItem(order);
        }
    }
}
