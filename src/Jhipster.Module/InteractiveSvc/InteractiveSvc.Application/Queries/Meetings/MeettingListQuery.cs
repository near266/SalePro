/*using AutoMapper;
using InteractiveSvc.Application.Contract.Contracts;
using InteractiveSvc.Application.Persistences;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InteractiveSvc.Application.Queries.Meetings
{
    public class MeettingListQuery : IRequest<List<MeetingResponse>>
    {
        public string? Id { get; set; }
    }
    public class MeettingListQueryHandler : IRequestHandler<MeettingListQuery, List<MeetingResponse>>
    {
        private readonly IFrameRepository _frameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MeettingListQueryHandler> _logger;

        public MeettingListQueryHandler(IFrameRepository frameRepository, IMapper mapper, ILogger<MeettingListQueryHandler> logger)
        {
            _frameRepository = frameRepository ?? throw new ArgumentNullException(nameof(frameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<List<MeetingResponse>> Handle(MeettingListQuery request, CancellationToken cancellationToken)
        {
            var response = _mapper.Map<List<MeetingResponse>>( await _frameRepository.ListMeeting(request.Id));
            return response;
        }
    }
}
*/