using AutoMapper;
using MediatR;
using OrderSvc.Application.Command.OrderCommand;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.OrderCommand
{
    public class DoPriceCommand : IRequest<PriceDto>
    {
        public List<Guid>? ProductId { get; set; }
        public Guid? VoucherId { get; set; }
    }

}
public class DoPriceCommandHandler : IRequestHandler<DoPriceCommand, PriceDto>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _repo;
    public DoPriceCommandHandler(IMapper mapper, IOrderRepository repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<PriceDto> Handle(DoPriceCommand rq, CancellationToken cancellationToken)
    {
        return await _repo.Price(rq.ProductId, rq.VoucherId);
    }
}