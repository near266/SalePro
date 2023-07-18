using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Command.ProductCommand;
using OrderSvc.Application.Query.ProductQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFF.Web.Controllers
{
    [ApiController]
    [Route("gateway/")]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public OrderController(IConfiguration configuration, ILogger<OrderController> logger, IMapper mapper, IMediator mediator)
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
        [HttpPost("Product/Create")]
        public async Task<ActionResult<int>> CreateProduct([FromBody] AddProductCommand rq)
        {
            try
            {

                rq.CreatedDate = DateTime.Now;
                rq.CreatedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPut("Product/Update")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand rq)
        {
            try
            {

                rq.LastModifiedDate = DateTime.Now;
                rq.LastModifiedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpDelete("Product/Delete")]
        public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductCommand rq)
        {
            try
            {


                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("Product/Search")]
        public async Task<IActionResult> SearchProduct([FromBody] SearchProductQuery rq)
        {
            try
            {


                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet("Product/GetDetail")]
        public async Task<IActionResult> ViewDetailProduct([FromQuery] GetProductDetailQuery rq)
        {
            try
            {


                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
