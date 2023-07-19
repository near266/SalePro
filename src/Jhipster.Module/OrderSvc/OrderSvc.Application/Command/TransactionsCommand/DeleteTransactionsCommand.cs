using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Command.VoucherCommand;
using OrderSvc.Application.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.TransactionsCommand
{
	public class DeleteTransactionsCommand : IRequest<int>
	{
		public Guid Id { get; set; }
	}
	public class DeleteTransactionsCommandHandle : IRequestHandler<DeleteTransactionsCommand, int>
	{
		private readonly ITransactionRepository _repo;
		private readonly IMapper _mapper;
		private readonly ILogger<DeleteTransactionsCommand> _logger;

		public DeleteTransactionsCommandHandle(ITransactionRepository repo, IMapper mapper, ILogger<DeleteTransactionsCommand> logger) 
		{
			_repo = repo;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<int> Handle(DeleteTransactionsCommand request, CancellationToken cancellationToken)
		{
			return await _repo.DeleteTransaction(request.Id);
		}
	}
}
