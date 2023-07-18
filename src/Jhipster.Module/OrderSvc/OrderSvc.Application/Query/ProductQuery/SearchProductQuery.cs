using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.ProductQuery
{
    public class SearchProductQuery :IRequest<PagedList<Product>>
    {
        public string? name { get; set; }
        public int page { get; set; }
        public int  pageSize { get; set; }
    }
    public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, PagedList<Product>>
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
        public async Task<PagedList<Product>> Handle(SearchProductQuery rq, CancellationToken cancellationToken)
        {
            return await _repo.SearchProduct(rq.name,rq.page,rq.pageSize);
        }

    }
}
