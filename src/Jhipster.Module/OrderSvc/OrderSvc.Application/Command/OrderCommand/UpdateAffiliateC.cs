using AutoMapper;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.OrderCommand
{
    public class UpdateAffiliateC:IRequest<Affiliates>
    {
        public Guid? Id { get; set; }
        public Guid? ReferrerId { get; set; }
        public Guid? ProviderId { get; set; }

        public Guid? BuyerId { get; set; }
        public Guid? SalerId { get; set; }
        public Guid? ParticipantsId { get; set; }

    }
    public class UpdateAffiliateCHandler : IRequestHandler<UpdateAffiliateC, Affiliates>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repo;
        public UpdateAffiliateCHandler(IMapper mapper, IOrderRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<Affiliates> Handle(UpdateAffiliateC rq, CancellationToken cancellationToken)
        {
            var res = _mapper.Map<Affiliates>(rq);
            var check = await _repo.UpdateAffi(res,cancellationToken);
            if (check != 0)
            {
                return res;
            }
            else
            {
                return res;

            }
        }
    }
}
