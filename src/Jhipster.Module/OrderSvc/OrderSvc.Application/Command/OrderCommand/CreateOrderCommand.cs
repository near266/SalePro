using AutoMapper;
using MediatR;
using OrderSvc.Application.Command.CategoryCommand;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.OrderCommand
{
    public class CreateOrderCommand : IRequest<Order>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public Guid? SalePerson { get; set; }
        public Guid? BoughtPerson { get; set; }
        public string? TransactionId { get; set; }
        public Guid? AffiliateId { get; set; }
        public Guid? VoucherId { get; set; }
        public int? Status { get; set; }
        [JsonIgnore]

        public string? CreatedBy { get; set; }
        [JsonIgnore]

        public DateTime CreatedDate { get; set; }
        [JsonIgnore]

        [MaxLength(100)]
        public string? LastModifiedBy { get; set; }
        [JsonIgnore]

        public DateTime? LastModifiedDate { get; set; }



    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repo;
        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<Order> Handle(CreateOrderCommand rq, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(rq);
            var res = await _repo.AddOrder(order);
            if (res != 0)
            {
                return order;
            }
            else
            {
                return new Order();
            }
        }
    }
}
