using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using InteractiveSvc.Application.Persistences;
using InteractiveSvc.Application.DTO;
using InteractiveSvc.Domain.Entities.Events;

namespace InteractiveSvc.Application.Queries.Events
{
    public class EventListJoinedQuery : IRequest<List<EventJoin>>
    {
        public string Id { get; set; }
    }
    public class EventListJoinedQueryHandler : IRequestHandler<EventListJoinedQuery, List<EventJoin>>
    {
        private readonly IFrameRepository _frameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EventListJoinedQueryHandler> _logger;

        public EventListJoinedQueryHandler(IFrameRepository frameRepository, IMapper mapper, ILogger<EventListJoinedQueryHandler> logger)
        {
            _frameRepository = frameRepository ?? throw new ArgumentNullException(nameof(frameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<List<EventJoin>> Handle(EventListJoinedQuery request, CancellationToken cancellationToken)
        {
            var response = await _frameRepository.EventListJoined(request.Id);
            return response;
        }
    }
}
