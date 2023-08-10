using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.OrderQuery
{
   public  class GetTotalQ : IRequest<TotalResponse>
    {
    }
    public class GetTotalQHandler : IRequestHandler<GetTotalQ, TotalResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPackageMember _repo;
        public GetTotalQHandler(IMapper mapper, IPackageMember repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<TotalResponse> Handle(GetTotalQ rq, CancellationToken cancellationToken)
        {
            return await _repo.GetTotal() ;
        }
    }
}
