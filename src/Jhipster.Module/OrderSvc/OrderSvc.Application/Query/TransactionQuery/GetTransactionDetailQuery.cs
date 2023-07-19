using AutoMapper;
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
	public class GetTransactionsDetailQuery : IRequest<Transactions>
	{
		public Guid Id { get; set; }
	}
	public class GetTransactionsDetailQueryHandler : IRequestHandler<GetTransactionsDetailQuery, Transactions>
	{
		private readonly ITransactionRepository _repo;
		private readonly IMapper _mapper;
		private readonly ILogger<GetTransactionsDetailQueryHandler> _logger;

		public GetTransactionsDetailQueryHandler(ITransactionRepository repo, IMapper mapper, ILogger<GetTransactionsDetailQueryHandler> logger)
		{
			_repo = repo;
			_mapper = mapper;
			_logger = logger;
		}
		public async Task<Transactions> Handle(GetTransactionsDetailQuery request, CancellationToken cancellationToken)
		{
			return await _repo.GetTransaction(request.Id);
		}
	}
}
