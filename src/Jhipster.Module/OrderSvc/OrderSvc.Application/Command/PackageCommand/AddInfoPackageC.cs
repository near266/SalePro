using AutoMapper;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.PackageCommand
{
    public class AddInfoPackageC : IRequest<InfoPackageMember>
    {
        public Guid? Id { get; set; }
        public Guid? ProfileMemberId { get; set; }
        public int? status { get; set; }
        public int? CurrentStatusMember { get; set; }
        public Guid? PackageId { get; set; }
    }
    public class AddInfoPackageCHandler : IRequestHandler<AddInfoPackageC, InfoPackageMember>
    {
        private readonly IMapper _mapper;
        private readonly IPackageMember _repo;
        public AddInfoPackageCHandler(IMapper mapper, IPackageMember repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<InfoPackageMember> Handle(AddInfoPackageC rq, CancellationToken cancellationToken)
        {
            var res = _mapper.Map<InfoPackageMember>(rq);
            return  await _repo.AddInfo(res);
           

        }
    }
}
