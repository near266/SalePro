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

namespace OrderSvc.Application.Query.PackageMemberQuery
{
    public class ViewDetailCustomerQuery : IRequest<ProfileCustomer>
    {
        public Guid Id { get; set; }
    }
    public class ViewDetailCustomerQueryHandler : IRequestHandler<ViewDetailCustomerQuery,ProfileCustomer>
    {
        private readonly IMapper _mapper;
        private readonly IPackageMember _repo;
        public ViewDetailCustomerQueryHandler(IMapper mapper, IPackageMember repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<ProfileCustomer> Handle(ViewDetailCustomerQuery rq, CancellationToken cancellationToken)
        {
            return await _repo.GetDetailCus(rq.Id);
        }
    }
}
