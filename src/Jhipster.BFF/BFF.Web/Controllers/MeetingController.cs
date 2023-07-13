using AutoMapper;
using BFF.Web.DTOs;
using InteractiveSvc.Application.Commands.Events;
using InteractiveSvc.Application.Commands.UserProfiles;
using InteractiveSvc.Application.Queries.Events;
using InteractiveSvc.Application.Queries.UserProfiles;
using Jhipster.Service.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BFF.Web.Controllers
{
    [ApiController]
    [Route("gateway/")]
    public class MeetingController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MeetingController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public MeetingController(ILogger<MeetingController> logger, IConfiguration configuration, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _configuration = configuration;
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

        [HttpPost("user/create")]
        [AllowAnonymous]
        public async Task<IActionResult> UserCreate([FromBody] UserCreateCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("user/{user-id}")]
        [Authorize]
        public async Task<IActionResult> UserDetail([FromRoute(Name = "user-id")] string Id)
        {
            try
            {
                var command = new UserDetailQuery();
                command.Id = Id;
                var response = await _mediator.Send(command);

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch("user/update")]
        [Authorize]
        public async Task<IActionResult> UserUpdate(UserUpdateCommand request)
        {
            try
            {
                var response = await _mediator.Send(request);

                return Ok(response);
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
                    var create = new UserCreateCommand();
                    create.Id = GetUserIdFromContext();
                    create.Username = GetUsernameFromContext();
                    var rep = await _mediator.Send(create);
                }
                catch
                {
                    _logger.LogError("Sth wrong to create");
                }

                var command = new UserDetailQuery();
                command.Id = GetUserIdFromContext();
                var response = await _mediator.Send(command);


                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("event/create")]
        [Authorize]
        public async Task<IActionResult> EventCreate([FromBody] EventCreateCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch("event/update")]
        [Authorize]
        public async Task<IActionResult> EventUpdate([FromBody] EventUpdateCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("event/{event-id}")]
        [Authorize]
        public async Task<IActionResult> EventDetail([FromRoute(Name = "event-id")] Guid Id)
        {
            try
            {
                var request = new EventDetailQuery();
                request.Id = Id;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpPost("events")]
        [Authorize]
        public async Task<IActionResult> EventList([FromBody] EventListQuery request)
        {
            try
            {
                request.Id = GetUserIdFromContext();
                var result = await _mediator.Send(request);
                if ((!string.IsNullOrEmpty(request.StartDate.ToString()) && request.StartDate != DateTime.MinValue)
                    && (!string.IsNullOrEmpty(request.EndDate.ToString()) && request.EndDate != DateTime.MinValue))
                {
                    var listDate = new List<EventDateDTO>();
                    foreach (var item in result)
                    {
                        var date = new EventDateDTO();
                        date.StartDate = item.StartDate;
                        date.EndDate = item.EndDate;
                        listDate.Add(date);
                    }
                    return Ok(listDate);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("event-joined/{user-id}")]
        [Authorize]
        public async Task<IActionResult> EventJoinedList([FromRoute(Name = "user-id")] string Id)
        {
            try
            {
                var request = new EventListJoinedQuery();
                request.Id = Id;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("event-join/create")]
        [Authorize]
        public async Task<IActionResult> EventJoinCreate([FromBody] EventJoinCreateCommand request)
        {
            try
            {
                request.CreatedBy = GetUsernameFromContext();
                var result = await _mediator.Send(request);


                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("event-join/{event-id}")]
        [Authorize]
        public async Task<IActionResult> EventListJoin([FromRoute(Name = "event-id")] Guid Id)
        {
            try
            {
                var request = new EventJoinListQuery();
                request.Id = Id;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


    }
}
