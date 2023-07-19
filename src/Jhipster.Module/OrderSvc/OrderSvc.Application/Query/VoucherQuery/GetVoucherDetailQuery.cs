using AutoMapper;
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
	public class GetVoucherDetailQuery : IRequest<Voucher>
	{
		public Guid Id { get; set; }
	}
	public class GetVoucherDetailQueryHandler : IRequestHandler<GetVoucherDetailQuery, Voucher>
	{
		private readonly IVoucherRepository _repo;
		private readonly IMapper _mapper;
		private readonly ILogger<GetVoucherDetailQueryHandler> _logger;

		public GetVoucherDetailQueryHandler(IVoucherRepository repo,IMapper mapper,ILogger<GetVoucherDetailQueryHandler>logger)
		{
			_repo = repo;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<Voucher> Handle(GetVoucherDetailQuery request, CancellationToken cancellationToken)
		{
			return await _repo.GetDetail(request.Id);
		}
	}
}
