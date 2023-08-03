using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.PackageMemberQuery
{
    public class ViewDetailCustomerQuery : IRequest<ProfileRes>
    {
        public Guid Id { get; set; }
    }
    public class ViewDetailCustomerQueryHandler : IRequestHandler<ViewDetailCustomerQuery, ProfileRes>
    {
        private readonly IMapper _mapper;
        private readonly IPackageMember _repo;
        public ViewDetailCustomerQueryHandler(IMapper mapper, IPackageMember repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<ProfileRes> Handle(ViewDetailCustomerQuery rq, CancellationToken cancellationToken)
        {
            var result = await _repo.GetDetailCus(rq.Id);
            var res = new ProfileRes();

            var map = _mapper.Map<ProfileRes>(result.profileCustomer);
            map.CompanyName=result.CompanyName;
            return map;
        }
    }
}
