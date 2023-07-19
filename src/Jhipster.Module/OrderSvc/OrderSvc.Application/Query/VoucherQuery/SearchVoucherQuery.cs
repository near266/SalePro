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
	public class SearchVoucherQuery : IRequest<PagedList<Voucher>>
	{
		public string? CodeVoucher { get; set; }
		public int page { get; set; }
		public int pageSize { get; set; }
	}
	public class SearchVoucherQueryHandler : IRequestHandler<SearchVoucherQuery, PagedList<Voucher>>
	{
		private readonly IVoucherRepository _repo;
		private readonly IMapper _mapper;
		private readonly ILogger<SearchVoucherQueryHandler> _logger;

		public SearchVoucherQueryHandler(IVoucherRepository repo,IMapper mapper,ILogger<SearchVoucherQueryHandler> logger)
		{
			_repo = repo;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<PagedList<Voucher>> Handle(SearchVoucherQuery request, CancellationToken cancellationToken)
		{
			return await _repo.SearchVoucher(request.CodeVoucher, request.page, request.pageSize);
		}
	}
}
