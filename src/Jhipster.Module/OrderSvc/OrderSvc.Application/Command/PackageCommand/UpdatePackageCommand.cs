using AutoMapper;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.PackageCommand
{
    public class UpdatePackageCommand :IRequest<int>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public int? Status { get; set; }

        public string PackageName { get; set; }
        public string Decripstion { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class UpdatePackageCommandHandler : IRequestHandler<UpdatePackageCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IPackageMember _repo;
        public UpdatePackageCommandHandler(IMapper mapper, IPackageMember repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<int> Handle(UpdatePackageCommand rq, CancellationToken cancellationToken)
        {
            var res = _mapper.Map<PackageMember>(rq);
            return await _repo.UpdatePackge(res);
        }
    }
}
