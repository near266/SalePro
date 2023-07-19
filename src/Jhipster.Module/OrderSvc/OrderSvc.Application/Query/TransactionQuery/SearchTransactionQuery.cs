using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Persistences;
using OrderSvc.Application.Query.ProductQuery;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.TransactionQuery
{
	public class SearchTransactionsQuery : IRequest<PagedList<Transactions>>
	{
		public string? type { get; set; }	
		public int page { get; set; }
		public int pageSize { get; set; }
	}
	public class SearchTransactionsQueryHandler : IRequestHandler<SearchTransactionsQuery, PagedList<Transactions>>
	{
		private readonly ITransactionRepository _repo;
		private readonly IMapper _mapper;
		private readonly ILogger<SearchTransactionsQueryHandler> _logger;

		public SearchTransactionsQueryHandler(ITransactionRepository repo, IMapper mapper, ILogger<SearchTransactionsQueryHandler> logger)
		{
			_repo = repo;
			_mapper = mapper;
			_logger = logger;
		}
		public async Task<PagedList<Transactions>> Handle(SearchTransactionsQuery request, CancellationToken cancellationToken)
		{
			return await _repo.Search(request.type, request.page, request.pageSize);
		}
	}
}
