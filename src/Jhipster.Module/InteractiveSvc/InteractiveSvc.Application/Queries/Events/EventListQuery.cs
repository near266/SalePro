using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using InteractiveSvc.Application.Persistences;
using InteractiveSvc.Application.DTO;
using InteractiveSvc.Domain.Entities.Events;
using System.Text.Json.Serialization;

namespace InteractiveSvc.Application.Queries.Events
{
    public class EventListQuery : IRequest<List<EventDTO>>
    {
        [JsonIgnore]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? SearchDate { get; set; }
        public bool? IsComing { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class EventListQueryHandler : IRequestHandler<EventListQuery, List<EventDTO>>
    {
        private readonly IFrameRepository _frameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EventListQueryHandler> _logger;

        public EventListQueryHandler(IFrameRepository frameRepository, IMapper mapper, ILogger<EventListQueryHandler> logger)
        {
            _frameRepository = frameRepository ?? throw new ArgumentNullException(nameof(frameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<List<EventDTO>> Handle(EventListQuery request, CancellationToken cancellationToken)
        {
            var response = await _frameRepository.ListEvent(request.Title, request.Description, request.IsComing, request.SearchDate, request.StartDate, request.EndDate);
            var ListEvent = new List<EventDTO>();
            _mapper.Map(response, ListEvent);
            if ((string.IsNullOrEmpty(request.StartDate.ToString()) || request.StartDate == DateTime.MinValue) && (string.IsNullOrEmpty(request.EndDate.ToString()) || request.StartDate == DateTime.MinValue))
            {
                var join = await _frameRepository.EventJoinListById(request.Id);
                foreach (var item in join)
                {
                    foreach (var item2 in ListEvent)
                    {
                        if (item.EventId == item2.Id) item2.Checked = true;
                    }
                }
            }

            return ListEvent;
        }
    }
}
