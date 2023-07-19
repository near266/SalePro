using AutoMapper;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.CompanyCommand
{
    public class DeleteCompanyCommand :IRequest<int>
    {
        public Guid Id { get;set; }
    }
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyReqository _repo;
        public DeleteCompanyCommandHandler(IMapper mapper, ICompanyReqository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<int> Handle(DeleteCompanyCommand rq, CancellationToken cancellationToken)
        {
            return await _repo.Delete(rq.Id);
        }
    }
}
