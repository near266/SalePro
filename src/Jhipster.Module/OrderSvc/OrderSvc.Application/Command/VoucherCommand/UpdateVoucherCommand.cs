using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Command.ProductCommand;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.VoucherCommand
{
	public class UpdateVoucherCommand : IRequest<int>
	{
		public Guid Id { get; set; }
		public Guid? VoucherProCusId { get; set; }
		public string CodeVoucher { get; set; }
		public decimal? Discount { get; set; }
		public int? Status { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? EndDate { get; set; }

		[JsonIgnore]
		[MaxLength(100)]
		public string? LastModifiedBy { get; set; }
		[JsonIgnore]
		public DateTime? LastModifiedDate { get; set; }
	}
	public class UpdateVoucherCommandHandler : IRequestHandler<UpdateVoucherCommand, int>
	{
		private readonly IVoucherRepository _repo;
		private readonly IMapper _mapper;
		private readonly ILogger<UpdateVoucherCommandHandler> _logger;

		public UpdateVoucherCommandHandler(IVoucherRepository repo, IMapper mapper, ILogger<UpdateVoucherCommandHandler> logger)
		{
			_repo = repo;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<int> Handle(UpdateVoucherCommand request, CancellationToken cancellationToken)
		{
			var value = _mapper.Map<Voucher>(request);
			return await _repo.UpdateVoucher(value);
		}
	}
}
