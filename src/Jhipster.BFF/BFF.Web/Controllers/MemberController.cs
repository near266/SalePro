using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Command.PackageCommand;
using OrderSvc.Application.Command.ProductCommand;
using OrderSvc.Application.Query.PackageMemberQuery;
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
        private readonly ILogger<MemberController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public MemberController(IConfiguration configuration, ILogger<MemberController> logger, IMapper mapper, IMediator mediator)
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

        [HttpPost("Member/Create")]
        public async Task<ActionResult<int>> CreateMember([FromBody] AddProfileCustomerCommand rq)
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
        [HttpGet("user/current")]
        [Authorize]
        public async Task<IActionResult> UserCurrent()
        {
            try
            {
                try
                {
                    var create = new AddProfileCustomerCommand();
                    create.Id = Guid.Parse(GetUserIdFromContext());
                    create.Username = GetUsernameFromContext();
                    var rep = await _mediator.Send(create);
                }
                catch
                {
                    _logger.LogError("Sth wrong to create");
                }

                var command = new ViewDetailCustomerQuery();
                command.Id = Guid.Parse(GetUserIdFromContext());
                var response = await _mediator.Send(command);


                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
                [HttpDelete("Member/Delete")]
        public async Task<ActionResult<int>> DeleteMember([FromBody] DeleteCustomerCommand rq)
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

        [HttpPut("Member/Update")]
        public async Task<ActionResult<int>> UpdateMember([FromBody] UpdateCustomerCommand rq)
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

        [HttpGet("Member/GetDetail")]
        public async Task<ActionResult<int>> DetailMember([FromQuery] ViewDetailCustomerQuery rq)
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
        [HttpPost("Member/GetAllOrSearch")]
        public async Task<IActionResult> SearchMember([FromBody] GetAllOrSearchQuery rq)
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
