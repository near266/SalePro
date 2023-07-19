using AutoMapper;
using MediatR;
using OrderSvc.Application.Command.CompanyCommand;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.CompanyQuery
{
    public class GetDetailCompanyQuery :IRequest<Company>
    {
        public Guid Id { get; set; }
    }
    public class GetDetailCompanyQueryHandler : IRequestHandler<GetDetailCompanyQuery, Company>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyReqository _repo;
        public GetDetailCompanyQueryHandler(IMapper mapper, ICompanyReqository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<Company> Handle(GetDetailCompanyQuery rq, CancellationToken cancellationToken)
        {
            return await _repo.GetDetail(rq.Id);
        }
    }
}
