//using AutoMapper;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using OrderSvc.Application.Command.OrderCommand;
//using OrderSvc.Application.Command.TransactionsCommand;
//using OrderSvc.Application.Command.VoucherCommand;
//using OrderSvc.Application.Query.TransactionQuery;
//using OrderSvc.Application.Query.VoucherQuery;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace OrderSvc.Controller
//{
//	[ApiController]
//	[Route("gateway/")]
//	public class TransactionsController : ControllerBase
//	{
//		private readonly IConfiguration _configuration;
//		private readonly ILogger<TransactionsController> _logger;
//		private readonly IMapper _mapper;
//		private readonly IMediator _mediator;

//		public TransactionsController(IConfiguration configuration, ILogger<TransactionsController> logger, IMapper mapper, IMediator mediator)
//		{
//			_configuration = configuration;
//			_logger = logger;
//			_mapper = mapper;
//			_mediator = mediator;
//		}
//		private string GetUsernameFromContext()
//		{
//			return User.FindFirst("name")?.Value;
//		}

//		private string GetUserIdFromContext()
//		{
//			return User.FindFirst("sub")?.Value;
//		}
//		private string GetUserIp()
//		{
//			return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
//		}
//		[HttpPost("Transactions/Create")]
//		public async Task<ActionResult<int>> CreateTransactions([FromBody] AddTransactionsCommand rq)
//		{
//			_logger.LogInformation($"REST request Create Transactions : {JsonConvert.SerializeObject(rq)}");
//			try
//			{
//				var res = await _mediator.Send(rq);
//				return Ok(res);
//			}
//			catch (Exception e)
//			{
//				_logger.LogError($"REST request Create Transactions fail: {e.Message}");
//				return StatusCode(500, e.Message);
//			}
//		}

//		[HttpPut("Transactions/Update")]
//		public async Task<IActionResult> UpdateTransactions([FromBody] UpdateTransactionsCommand rq)
//		{
//			_logger.LogInformation($"REST request Update Transactions : {JsonConvert.SerializeObject(rq)}");
//			try
//			{
//				var res = await _mediator.Send(rq);
//				return Ok(res);
//			}
//			catch (Exception e)
//			{
//				_logger.LogError($"REST request Update Transactions fail: {e.Message}");
//				return StatusCode(500, e.Message);
//			}
//		}

//		[HttpDelete("Transactions/Delete")]
//		public async Task<IActionResult> DeleteTransactions([FromBody] DeleteTransactionsCommand rq)
//		{
//			_logger.LogInformation($"REST request Delete Transactions : {JsonConvert.SerializeObject(rq)}");
//			try
//			{
//				var res = await _mediator.Send(rq);
//				return Ok(res);
//			}
//			catch (Exception e)
//			{
//				_logger.LogError($"REST request Delete Transactions fail: {e.Message}");
//				return StatusCode(500, e.Message);
//			}
//		}
//		[HttpPost("Transactions/Search")]
//		public async Task<IActionResult> SearchTransactions([FromBody] PriceIdComand rq)
//		{
//			_logger.LogInformation($"REST request Search Transactions : {JsonConvert.SerializeObject(rq)}");
//			try
//			{
//				var res = await _mediator.Send(rq);
//				return Ok(res);
//			}
//			catch (Exception e)
//			{
//				_logger.LogError($"REST request Search Transactions fail: {e.Message}");
//				return StatusCode(500, e.Message);
//			}
//		}
//		[HttpGet("Transactions/GetDetail")]
//		public async Task<IActionResult> ViewDetailTransactions([FromQuery] GetTransactionsDetailQuery rq)
//		{
//			_logger.LogInformation($"REST request Get Detail Transactions : {JsonConvert.SerializeObject(rq)}");
//			try
//			{
//				var res = await _mediator.Send(rq);
//				return Ok(res);
//			}
//			catch (Exception e)
//			{
//				_logger.LogError($"REST request Get Detail Transactions fail: {e.Message}");
//				return StatusCode(500, e.Message);
//			}
//		}
//	}
//}
