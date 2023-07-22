using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.ProductQuery
{
    public class SearchProductQuery :IRequest<PagedList<ProductDTO>>
    {
        public string? name { get; set; }
        public int page { get; set; }
        public int  pageSize { get; set; }
    }
    public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, PagedList<ProductDTO>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<SearchProductQueryHandler> _logger;

        public SearchProductQueryHandler(IProductRepository repo, IMapper mapper, ILogger<SearchProductQueryHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PagedList<ProductDTO>> Handle(SearchProductQuery rq, CancellationToken cancellationToken)
        {
            var res= await _repo.SearchProduct(rq.name,rq.page,rq.pageSize);
            var map = _mapper.Map<PagedList<ProductDTO>>(res);
            return res;
        }

    }
}
