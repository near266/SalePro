using AutoMapper;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.OrderCommand
{
    public class PriceIdComand : IRequest<Payment>
    {
        public Guid Id { get; set; }
    }
    public class DoPriceCommandHandler : IRequestHandler<PriceIdComand, Payment>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repo;
        public DoPriceCommandHandler(IMapper mapper, IOrderRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<Payment> Handle(PriceIdComand rq, CancellationToken cancellationToken)
        {
            return await _repo.PriceId(rq.Id);
        }
    }
}
