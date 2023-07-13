using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using InteractiveSvc.Application.Persistences;
using InteractiveSvc.Application.DTO;
using InteractiveSvc.Domain.Entities.Events;

namespace InteractiveSvc.Application.Queries.Events
{
    public class EventDetailQuery : IRequest<Event>
    {
        public Guid Id { get; set; }
    }
    public class EventDetailQueryHandler : IRequestHandler<EventDetailQuery, Event>
    {
        private readonly IFrameRepository _frameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EventDetailQueryHandler> _logger;

        public EventDetailQueryHandler(IFrameRepository frameRepository, IMapper mapper, ILogger<EventDetailQueryHandler> logger)
        {
            _frameRepository = frameRepository ?? throw new ArgumentNullException(nameof(frameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Event> Handle(EventDetailQuery request, CancellationToken cancellationToken)
        {
            var response = await _frameRepository.DetailEvent(request.Id);
            return response;
        }
    }
}
