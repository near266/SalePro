using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.PackageMemberQuery
{
    public class GetCurrentStatusByIdUserQ :IRequest<PackageDto>
    {
        public Guid Id { get; set; }
    }
    public class PackageDtoHandler : IRequestHandler<GetCurrentStatusByIdUserQ, PackageDto>
    {
        private readonly IMapper _mapper;
        private readonly IPackageMember _repo;
        public PackageDtoHandler(IMapper mapper, IPackageMember repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<PackageDto> Handle(GetCurrentStatusByIdUserQ rq, CancellationToken cancellationToken)
        {
            var reslut = await _repo.GetCurrentStatusByIdUser(rq.Id);



            return reslut;
        }
    }
}
