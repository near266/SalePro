using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Command.VoucherCommand;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Transactions;

namespace OrderSvc.Application.Command.TransactionsCommand
{
	public class AddTransactionsCommand : IRequest<Transactions>
	{
        public string? TransactionName { get; set; }

        public string TransactionType { get; set; }
		public double? TotalAmount { get; set; }
		public DateTime? TransactionDate { get; set; }
		public string? PaymentMethod { get; set; }

	}
	public class AddTransactionsCommandHandler : IRequestHandler<AddTransactionsCommand, Transactions>
	{
		private readonly ITransactionRepository _repo;
		private readonly IMapper _mapper;
		private readonly ILogger<AddTransactionsCommand> _logger;

		public AddTransactionsCommandHandler(ITransactionRepository repo, IMapper mapper, ILogger<AddTransactionsCommand> logger)
		{
			_repo = repo;
			_mapper = mapper;
			_logger = logger;
		}
		public async Task<Transactions> Handle(AddTransactionsCommand request, CancellationToken cancellationToken)
		{
			var value = _mapper.Map<Transactions>(request);
			 return await _repo.AddTransaction(value);
		}
	}
}
