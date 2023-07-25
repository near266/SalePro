using AutoMapper;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.CategoryCommand
{
    public class DeleteCateProCommand :IRequest<int>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCateProCommandHandler : IRequestHandler<DeleteCateProCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repo;
        public DeleteCateProCommandHandler(IMapper mapper, ICategoryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<int> Handle(DeleteCateProCommand rq, CancellationToken cancellationToken)
        {
            return await _repo.Delete(rq.Id);
        }
    }
}
