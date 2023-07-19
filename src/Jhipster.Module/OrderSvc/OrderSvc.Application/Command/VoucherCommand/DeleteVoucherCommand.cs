using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Command.ProductCommand;
using OrderSvc.Application.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.VoucherCommand
{
	public class DeleteVoucherCommand : IRequest<int>
	{
		public Guid Id { get; set; }
	}
	public class DeleteVoucherCommandHandler : IRequestHandler<DeleteVoucherCommand, int>
	{
		private readonly IVoucherRepository _repo;
		private readonly IMapper _mapper;
		private readonly ILogger<DeleteVoucherCommandHandler> _logger;

		public DeleteVoucherCommandHandler(IVoucherRepository repo, IMapper mapper, ILogger<DeleteVoucherCommandHandler> logger)
		{
			_repo = repo;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<int> Handle(DeleteVoucherCommand request, CancellationToken cancellationToken)
		{
			return await _repo.DeleteVoucher(request.Id);
		}
	}
}
