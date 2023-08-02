using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Microsoft.VisualBasic;
using OrderSvc.Application.Persistences;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.OrderQuery
{
    public class SearchOrGetAllQuery : IRequest<PagedList<SearchOrder>>
    {
        public string? name { get; set; }
        public int? status { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class SearchOrGetAllQueryHandler : IRequestHandler<SearchOrGetAllQuery, PagedList<SearchOrder>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repo;
        public SearchOrGetAllQueryHandler(IMapper mapper, IOrderRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<PagedList<SearchOrder>> Handle(SearchOrGetAllQuery rq, CancellationToken cancellationToken)
        {
            return await _repo.SearchOrder(rq.name, rq.status, rq.page, rq.pageSize);
        }
    }
}
