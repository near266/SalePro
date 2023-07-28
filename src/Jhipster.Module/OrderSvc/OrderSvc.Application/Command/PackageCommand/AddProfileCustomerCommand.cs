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
    public class AddProfileCustomerCommand :IRequest<ProfileCustomer>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

    }
    public class AddProfileCustomerCommandHandler : IRequestHandler<AddProfileCustomerCommand, ProfileCustomer>
    {
        private readonly IMapper _mapper;
        private readonly IPackageMember _repo;
        public AddProfileCustomerCommandHandler(IMapper mapper, IPackageMember repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<ProfileCustomer> Handle(AddProfileCustomerCommand rq, CancellationToken cancellationToken)
        {
            var res = _mapper.Map<ProfileCustomer>(rq);
            var check = await _repo.AddCus(res);
            return check;
             
        }
    }
}
