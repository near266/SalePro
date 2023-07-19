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
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.TransactionsCommand
{
	public class UpdateTransactionsCommand : IRequest<int>
	{
		public string TransactionId { get; set; }
		public string TransactionType { get; set; }
		public decimal? TotalAmount { get; set; }
		public DateTime? TransactionDate { get; set; }
		public string? PaymentMethod { get; set; }
	}
	public class UpdateTransactionsCommandHandler : IRequestHandler<UpdateTransactionsCommand, int>
	{
		private readonly ITransactionRepository _repo;
		private readonly IMapper _mapper;
		private readonly ILogger<UpdateTransactionsCommand> _logger;

		public UpdateTransactionsCommandHandler(ITransactionRepository repo, IMapper mapper, ILogger<UpdateTransactionsCommand> logger)
		{

			_repo = repo;
			_mapper = mapper;
			_logger = logger;
		}
		public async Task<int> Handle(UpdateTransactionsCommand request, CancellationToken cancellationToken)
		{
			var value = _mapper.Map<Transactions>(request);
			return await _repo.UpdateTransaction(value);
		}
	}
}
