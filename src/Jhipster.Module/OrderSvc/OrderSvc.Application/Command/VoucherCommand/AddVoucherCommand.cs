﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.VoucherCommand
{
	public class AddVoucherCommand : IRequest<int>
	{
		[JsonIgnore]
		public Guid Id { get; set; }
		public Guid? VoucherProCusId { get; set; }
		public string CodeVoucher { get; set; }
		public decimal? Discount { get; set; }
		public int? Status { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
	public class AddVoucherCommandHandler : IRequestHandler<AddVoucherCommand, int>
	{
		private readonly IVoucherRepository _repo;
		private readonly IMapper _mapper;
		private readonly ILogger<AddVoucherCommand> _logger;

		public AddVoucherCommandHandler(IVoucherRepository repo,IMapper mapper,ILogger<AddVoucherCommand>logger)
		{
			_repo = repo;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<int> Handle(AddVoucherCommand request, CancellationToken cancellationToken)
		{
			var value = _mapper.Map<Voucher>(request);
			return await _repo.AddVoucher(value);
		}
	}
}