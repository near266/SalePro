/*using AutoMapper;
using InteractiveSvc.Application.Contract.Contracts;
using InteractiveSvc.Application.Persistences;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InteractiveSvc.Application.Queries.Meetings
{
    public class MeetingDetailQuery : IRequest<MeetingResponse>
    {
        public Guid? Id { get; set; }
    }
    public class MeetingDetailQueryHandler : IRequestHandler<MeetingDetailQuery, MeetingResponse>
    {
        private readonly IFrameRepository _frameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MeetingDetailQueryHandler> _logger;

        public MeetingDetailQueryHandler(IFrameRepository frameRepository, IMapper mapper, ILogger<MeetingDetailQueryHandler> logger)
        {
            _frameRepository = frameRepository ?? throw new ArgumentNullException(nameof(frameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<MeetingResponse> Handle(MeetingDetailQuery request, CancellationToken cancellationToken)
        {
            var response = _mapper.Map<MeetingResponse>(await _frameRepository.DetailMeeting(request.Id));
            return response;
        }
    }
}
*/