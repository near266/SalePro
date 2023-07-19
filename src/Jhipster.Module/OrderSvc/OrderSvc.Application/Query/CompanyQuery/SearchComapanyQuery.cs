using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.CompanyQuery
{
    public class SearchComapanyQuery : IRequest<PagedList<Company>>
    {
        public string? name { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }   
    }
    public class SearchComapanyQueryHandler : IRequestHandler<SearchComapanyQuery, PagedList<Company>>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyReqository _repo;
        public SearchComapanyQueryHandler(IMapper mapper, ICompanyReqository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<PagedList<Company>> Handle(SearchComapanyQuery rq, CancellationToken cancellationToken)
        {
            return await _repo.Search(rq.name,rq.page,rq.pageSize);
        }
    }
}
