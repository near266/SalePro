using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Command.ProductCommand;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.VoucherCommand
{
	public class UpdateVoucherCommand : IRequest<Voucher>
	{
        public Guid Id { get; set; }
        public Guid? VoucherProCusId { get; set; }
        public string? CodeVoucher { get; set; }
        public List<string>? Image { get; set; }
        public string? Description { get; set; }
        public decimal? Discount { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
	public class UpdateVoucherCommandHandler : IRequestHandler<UpdateVoucherCommand, Voucher>
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

		public async Task<Voucher> Handle(UpdateVoucherCommand request, CancellationToken cancellationToken)
		{
			var value = _mapper.Map<Voucher>(request);
			await _repo.UpdateVoucher(value);
			return value;
		}
	}
}
