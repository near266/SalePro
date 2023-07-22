using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Command.ProductCommand;
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
    public class GetProductDetailQuery :IRequest<ProductDTO>
    {
        public Guid Id { get; set; }
    }
    public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, ProductDTO>
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
        public async Task<ProductDTO> Handle(GetProductDetailQuery rq,CancellationToken cancellationToken)
        {
            var res= await _repo.GetDetail(rq.Id);
            return res;
        }

    }
}
