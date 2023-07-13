using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using InteractiveSvc.Application.Contract.Contracts;
using InteractiveSvc.Application.Persistences;
using InteractiveSvc.Domain.Entities;

namespace InteractiveSvc.Application.Queries.UserProfiles
{
    public class UserDetailQuery : IRequest<UserProfile>
    {
        public string? Id { get; set; }
    }
    public class UserDetailQueryHandler : IRequestHandler<UserDetailQuery, UserProfile>
    {
        private readonly IFrameRepository _frameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserDetailQueryHandler> _logger;

        public UserDetailQueryHandler(IFrameRepository frameRepository, IMapper mapper, ILogger<UserDetailQueryHandler> logger)
        {
            _frameRepository = frameRepository ?? throw new ArgumentNullException(nameof(frameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<UserProfile> Handle(UserDetailQuery request, CancellationToken cancellationToken)
        {
            var response =  await _frameRepository.DetailUser(request.Id);
            return response;
        }
    }
}
