using AutoMapper;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.PackageCommand
{
    public class UpdateInfoPackageC:IRequest<PackageDto>
    {
        public Guid? Id { get; set; }
        public Guid? ProfileMemberId { get; set; }
        public int? status { get; set; }
        public int? CurrentStatusMember { get; set; }
        public Guid? PackageId { get; set; }
    }
    public class UpdateInfoPackageCHandle : IRequestHandler<UpdateInfoPackageC, PackageDto>
    {
        private readonly IMapper _mapper;
        private readonly IPackageMember _repo;
        public UpdateInfoPackageCHandle(IMapper mapper, IPackageMember repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<PackageDto> Handle(UpdateInfoPackageC rq, CancellationToken cancellationToken)
        {
            var res = _mapper.Map<InfoPackageMember>(rq);
            var check = await _repo.UpdateInfo(res);
            return await _repo.GetInfoPackageMember(rq.Id);

        }
    }
}
