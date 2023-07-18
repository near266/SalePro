using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Command.ProductCommand;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.ProductQuery
{
    public class GetProductDetailQuery :IRequest<Product>
    {
        public Guid Id { get; set; }
    }
    public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery,Product>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductDetailQueryHandler> _logger;

        public GetProductDetailQueryHandler(IProductRepository repo, IMapper mapper, ILogger<GetProductDetailQueryHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Product> Handle(GetProductDetailQuery rq,CancellationToken cancellationToken)
        {
            return await _repo.GetDetail(rq.Id);
        }

    }
}
