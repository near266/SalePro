using AutoMapper;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.OrderCommand
{
    public class UpdateStatusOrderCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public Guid? SalePerson { get; set; }
        public Guid? BoughtPerson { get; set; }
        public string? TransactionId { get; set; }
        public Guid? AffiliateId { get; set; }
        public Guid? VoucherId { get; set; }
        public int? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(100)]
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
    public class UpdateStatusOrderCommandHandler : IRequestHandler<UpdateStatusOrderCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repo;
        public UpdateStatusOrderCommandHandler(IMapper mapper, IOrderRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<int> Handle(UpdateStatusOrderCommand rq, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(rq);
            var res = await _repo.UpdateOrder(order, cancellationToken);

            return res;


        }
    }
}
