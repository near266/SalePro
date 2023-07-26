using AutoMapper;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Application.Query.CompanyQuery;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.OrderQuery
{
    public class ViewDetailOrderQuery : IRequest<OrderResponse>
    {
        public Guid Id { get; set; }   
    }
    public class ViewDetailOrderQueryHandler : IRequestHandler<ViewDetailOrderQuery, OrderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repo;
        public ViewDetailOrderQueryHandler(IMapper mapper, IOrderRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<OrderResponse> Handle(ViewDetailOrderQuery rq, CancellationToken cancellationToken)
        {
            return await _repo.GetOrderById(rq.Id);
        }
    }
}
