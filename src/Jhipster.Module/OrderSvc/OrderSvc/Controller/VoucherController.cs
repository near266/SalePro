using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Controller
{
	public class VoucherController : ControllerBase
	{
		private readonly ILogger<VoucherController> _logger;
		private readonly IDistributedCache _cache;
		private readonly IConfiguration _configuration;
		private readonly IMediator _mediator;

		public VoucherController(ILogger<VoucherController> logger, IDistributedCache cache, IConfiguration configuration, IMediator mediator)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_cache = cache ?? throw new ArgumentNullException(nameof(cache));
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}
	}
}
