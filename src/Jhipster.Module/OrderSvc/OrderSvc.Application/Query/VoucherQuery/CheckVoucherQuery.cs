using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.VoucherQuery
{
    public class CheckVoucherQuery:IRequest<IEnumerable<Voucher>>
    {
    }
    public class CheckVoucherQueryHandler : IRequestHandler<CheckVoucherQuery, IEnumerable<Voucher>>
    {
        private readonly IVoucherRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckVoucherQuery> _logger;

        public CheckVoucherQueryHandler(IVoucherRepository repo, IMapper mapper, ILogger<CheckVoucherQuery> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Voucher>> Handle(CheckVoucherQuery request, CancellationToken cancellationToken)
        {
            return await _repo.CheckVoucherExp();
            
        }
    }
}
