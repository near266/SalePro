using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using InteractiveSvc.Application.Persistences;
using InteractiveSvc.Application.DTO;
using InteractiveSvc.Domain.Entities.Events;
using System.Text.Json.Serialization;

namespace InteractiveSvc.Application.Commands.Events
{
    public class EventJoinCreateCommand : IRequest<EventJoinDTO>
    {
        public string UserId { get; set; }
        public Guid EventId { get; set; }
        public string? Location { get; set; }
        [JsonIgnore]
        public DateTime? CheckInDate { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
    }
    public class EventJoinCreateCommandHandler : IRequestHandler<EventJoinCreateCommand, EventJoinDTO>
    {
        private readonly IFrameRepository _frameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EventJoinCreateCommandHandler> _logger;

        public EventJoinCreateCommandHandler(IFrameRepository frameRepository, IMapper mapper, ILogger<EventJoinCreateCommandHandler> logger)
        {
            _frameRepository = frameRepository ?? throw new ArgumentNullException(nameof(frameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<EventJoinDTO> Handle(EventJoinCreateCommand request, CancellationToken cancellationToken)
        {
            var value = _mapper.Map<EventJoin>(request);
            value.CheckedInLocation = request.Location;
            var response = await _frameRepository.CreateEventJoin(value, cancellationToken);
            var ListEvent = new EventJoinDTO();
            _mapper.Map(response, ListEvent);
            if (response != null) ListEvent.Events.Checked = true;
            return ListEvent;
        }
    }
}
