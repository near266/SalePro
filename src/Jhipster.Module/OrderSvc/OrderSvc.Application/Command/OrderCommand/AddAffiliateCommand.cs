using AutoMapper;
using MediatR;
using OrderSvc.Application.Command.CompanyCommand;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.OrderCommand
{
    public class AddAffiliateCommand :IRequest<int>
    {
        [JsonIgnore]
        public Guid? Id { get; set; }
        public Guid? ReferrerId { get; set; }
        public Guid? BuyerId { get; set; }
        public Guid? SalerId { get; set; }
        public Guid? ParticipantsId { get; set; }
    }
    public class AddAffiliateCommandHandler : IRequestHandler<AddAffiliateCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repo;
        public AddAffiliateCommandHandler(IMapper mapper, IOrderRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<int> Handle(AddAffiliateCommand rq, CancellationToken cancellationToken)
        {
            var res = _mapper.Map<Affiliates>(rq);
            return await _repo.AddAffi(res);
        }
    }

}
