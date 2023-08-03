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
    public class UpdateCustomerCommand :IRequest<int>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string Username { get; set; }
        public DateTime? DOB { get; set; }

        public Guid? CompanyId { get; set; }

        [JsonIgnore]
        public Company? Company { get; set; }
        public string? Position { get; set; }
        public string? Decripstion { get; set; }

        [MaxLength(int.MaxValue)]
        public string? Avatar { get; set; }
        public string? ProductName { get; set; }

        public string? coverImage { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public int? Role { get; set; }
        public int? memberShip { get; set; }

    }
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IPackageMember _repo;
        public UpdateCustomerCommandHandler(IMapper mapper, IPackageMember repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<int> Handle(UpdateCustomerCommand rq, CancellationToken cancellationToken)
        {
            var res = _mapper.Map<ProfileCustomer>(rq);
            var check = await _repo.UpdateCus(res);
          return check;
        }
    }
}
