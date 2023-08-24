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
    public class UpdateOrderItemC : IRequest<OrderItem>
    {
        public Guid Id { get; set; }

        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class UpdateOrderItemCHandler : IRequestHandler<UpdateOrderItemC, OrderItem>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repo;
        public UpdateOrderItemCHandler(IMapper mapper, IOrderRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<OrderItem> Handle(UpdateOrderItemC rq, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<OrderItem>(rq);
            return await _repo.UpdateOrderItem(order);
        }
    }
}
