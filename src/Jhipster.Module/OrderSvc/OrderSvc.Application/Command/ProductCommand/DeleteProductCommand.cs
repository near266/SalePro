using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.ProductCommand
{
    public class DeleteProductCommand :IRequest<int>
    {
        public Guid Id { get; set; }
    }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IProductRepository repo, IMapper mapper, ILogger<DeleteProductCommandHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<int> Handle(DeleteProductCommand rq, CancellationToken cancellationToken)
        {

            return await _repo.Delete(rq.Id);
        }

    }
}
