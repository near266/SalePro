using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using InteractiveSvc.Application.Persistences;
using InteractiveSvc.Application.DTO;
using InteractiveSvc.Domain.Entities.Events;

namespace InteractiveSvc.Application.Queries.Events
{
    public class EventJoinListQuery : IRequest<List<EventJoin>>
    {
        public Guid? Id { get; set; }
    }
    public class EventJoinListQueryHandler : IRequestHandler<EventJoinListQuery, List<EventJoin>>
    {
        private readonly IFrameRepository _frameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EventJoinListQueryHandler> _logger;

        public EventJoinListQueryHandler(IFrameRepository frameRepository, IMapper mapper, ILogger<EventJoinListQueryHandler> logger)
        {
            _frameRepository = frameRepository ?? throw new ArgumentNullException(nameof(frameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<List<EventJoin>> Handle(EventJoinListQuery request, CancellationToken cancellationToken)
        {
            var response = await _frameRepository.EventJoinList(request.Id);
            return response;
        }
    }
}
