﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using InteractiveSvc.Application.Persistences;
using InteractiveSvc.Application.DTO;
using InteractiveSvc.Domain.Entities.Events;

namespace InteractiveSvc.Application.Commands.Events
{
    public class EventUpdateCommand : IRequest<Event>
    {
        public Guid Id { get; set; }
        public Guid? MeetingId { get; set; }
        public string? Title { get; set; }
        public string? Background { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public int? Quantity { get; set; }
    }
    public class EventUpdateCommandHandler : IRequestHandler<EventUpdateCommand, Event>
    {
        private readonly IFrameRepository _frameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EventUpdateCommandHandler> _logger;

        public EventUpdateCommandHandler(IFrameRepository frameRepository, IMapper mapper, ILogger<EventUpdateCommandHandler> logger)
        {
            _frameRepository = frameRepository ?? throw new ArgumentNullException(nameof(frameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Event> Handle(EventUpdateCommand request, CancellationToken cancellationToken)
        {
            var value = _mapper.Map<EventUpdateDTO>(request);
            var response = await _frameRepository.UpdateEvent(request.Id, value, cancellationToken);
            return response;
        }
    }
}
