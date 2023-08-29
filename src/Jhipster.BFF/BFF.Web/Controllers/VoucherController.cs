using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderSvc.Application.Command.ProductCommand;
using OrderSvc.Application.Command.VoucherCommand;
using OrderSvc.Application.Query.ProductQuery;
using OrderSvc.Application.Query.VoucherQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Controller
{
	[ApiController]
	[Route("gateway/")]

	public class VoucherController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger<VoucherController> _logger;
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;

		public VoucherController(IConfiguration configuration, ILogger<VoucherController> logger, IMapper mapper, IMediator mediator)
		{
			_configuration = configuration;
			_logger = logger;
			_mapper = mapper;
			_mediator = mediator;
		}
		private string GetUsernameFromContext()
		{
			return User.FindFirst("name")?.Value;
		}

		private string GetUserIdFromContext()
		{
			return User.FindFirst("sub")?.Value;
		}
		private string GetUserIp()
		{
			return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
		}

		[HttpPost("Voucher/Create")]
		public async Task<ActionResult<int>> CreateVoucher([FromBody] AddVoucherCommand rq)
		{
			_logger.LogInformation($"REST request Create Voucher : {JsonConvert.SerializeObject(rq)}");
			try
			{
				var res = await _mediator.Send(rq);
				return Ok(res);
			}
			catch (Exception e)
			{
				_logger.LogError($"REST request Create Voucher fail: {e.Message}");
				return StatusCode(500, e.Message);
			}
		}
		[HttpPut("Voucher/Update")]
		public async Task<IActionResult> UpdateVoucher([FromBody] UpdateVoucherCommand rq)
		{
			_logger.LogInformation($"REST request Update Voucher : {JsonConvert.SerializeObject(rq)}");
			try
			{
				var res = await _mediator.Send(rq);
				return Ok(res);
			}
			catch (Exception e)
			{
				_logger.LogError($"REST request Update Voucher fail: {e.Message}");
				return StatusCode(500, e.Message);
			}
		}
		[HttpDelete("Voucher/Delete")]
		public async Task<IActionResult> DeleteVoucher([FromBody] DeleteVoucherCommand rq)
		{
			_logger.LogInformation($"REST request Delete Voucher : {JsonConvert.SerializeObject(rq)}");
			try
			{
				var res = await _mediator.Send(rq);
				return Ok(res);
			}
			catch (Exception e)
			{
				_logger.LogError($"REST request Delete Voucher fail: {e.Message}");
				return StatusCode(500, e.Message);
			}
		}
		[HttpPost("Voucher/Search")]
		public async Task<IActionResult> SearchVoucher([FromBody] SearchVoucherQuery rq)
		{
			_logger.LogInformation($"REST request Search Voucher : {JsonConvert.SerializeObject(rq)}");
			try
			{
			var check = new CheckVoucherQuery() { };
			 var checkres=	await _mediator.Send(check);
				if(checkres!=null)
				{

                foreach (var item in checkres)
                {

                    var up = new UpdateVoucherCommand()
                    {
                        Id = item.Id,
                        isExpired = 0,
                    };
                    await _mediator.Send(up);
                }
				}
                var res = await _mediator.Send(rq);
				return Ok(res);
			}
			catch (Exception e)
			{
				_logger.LogError($"REST request Search Voucher fail: {e.Message}");
				return StatusCode(500, e.Message);
			}
		}
		[HttpGet("Voucher/GetDetail")]
		public async Task<IActionResult> ViewDetailVoucher([FromQuery] GetVoucherDetailQuery rq)
		{
			_logger.LogInformation($"REST request Get Detail Voucher : {JsonConvert.SerializeObject(rq)}");
			try
			{
				var res = await _mediator.Send(rq);
				return Ok(res);
			}
			catch (Exception e)
			{
				_logger.LogError($"REST request Get Detail Voucher fail: {e.Message}");
				return StatusCode(500, e.Message);
			}
		}
        [HttpGet("Voucher/Check")]
        public async Task<IActionResult> ViewCheck([FromQuery] CheckVoucherQuery rq)
        {
            _logger.LogInformation($"REST request Get Detail Voucher : {JsonConvert.SerializeObject(rq)}");
            try
            {
                var res = await _mediator.Send(rq);
				foreach (var item in res)
				{

					var up = new UpdateVoucherCommand()
					{
						Id = item.Id,
						isExpired = 0,
					};
					await _mediator.Send(up);
				} 
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError($"REST request Get Detail Voucher fail: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }


    }
}
