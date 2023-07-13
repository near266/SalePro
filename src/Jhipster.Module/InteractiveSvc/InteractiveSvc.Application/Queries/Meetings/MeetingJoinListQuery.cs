/*using AutoMapper;
using InteractiveSvc.Application.Contract.Contracts;
using InteractiveSvc.Application.Persistences;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InteractiveSvc.Application.Queries.Meetings
{
    public class MeetingJoinListQuery : IRequest<List<MeetingJoinResponse>>
    {
        public string? Id { get; set; }
    }
    public class MeetingJoinListQueryHandler : IRequestHandler<MeetingJoinListQuery, List<MeetingJoinResponse>>
    {
        private readonly IFrameRepository _frameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MeetingJoinListQueryHandler> _logger;

        public MeetingJoinListQueryHandler(IFrameRepository frameRepository, IMapper mapper, ILogger<MeetingJoinListQueryHandler> logger)
        {
            _frameRepository = frameRepository ?? throw new ArgumentNullException(nameof(frameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<List<MeetingJoinResponse>> Handle(MeetingJoinListQuery request, CancellationToken cancellationToken)
        {
            var response = _mapper.Map<List<MeetingJoinResponse>>( await _frameRepository.ListMeetingJoin(request.Id));
            return response;
        }
    }
}
*/