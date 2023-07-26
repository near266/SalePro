using AutoMapper;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.PackageCommand
{
    public class UpdateCustomerCommand :IRequest<ProfileCustomer>
    {
        public Guid? Id { get; set; }
        public string CustomerName { get; set; }
        public Guid? CompanyId { get; set; }
        [JsonIgnore]
        public Company? Company { get; set; }
        public string? Position { get; set; }

        public string? Decripstion { get; set; }

        [MaxLength(int.MaxValue)]
        public string? Avatar
        {
            get; set;
        }
    }
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ProfileCustomer>
    {
        private readonly IMapper _mapper;
        private readonly IPackageMember _repo;
        public UpdateCustomerCommandHandler(IMapper mapper, IPackageMember repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<ProfileCustomer> Handle(UpdateCustomerCommand rq, CancellationToken cancellationToken)
        {
            var res = _mapper.Map<ProfileCustomer>(rq);
            var check = await _repo.UpdateCus(res);
            if (check != 0)
            {
                return res;
            }
            else
            {
                throw new ArgumentException(" fail");
            }
        }
    }
}
