using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Command.PackageCommand;
using OrderSvc.Application.Command.ProductCommand;
using OrderSvc.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFF.Web.Controllers
{
    [ApiController]
    [Route("gateway/")]
    public class MemberController :ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TransactionsController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public MemberController(IConfiguration configuration, ILogger<TransactionsController> logger, IMapper mapper, IMediator mediator)
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


        [HttpPost("Package/Create")]
        public async Task<ActionResult<int>> CreatePackage([FromBody] CreatePackageCommand rq)
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
        [HttpPost("Package/Approve")]
        public async Task<ActionResult<int>> ApprovePackge([FromBody] UpdatePackageCommand rq)
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
