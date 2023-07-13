using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using InteractiveSvc.Domain.Entities;
using InteractiveSvc.Application.Contract.Contracts;
using InteractiveSvc.Application.Persistences;

namespace InteractiveSvc.Application.Commands.UserProfiles
{
    public class UserCreateCommand : IRequest<UserProfile>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, UserProfile>
    {
        private readonly IFrameRepository _frameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserCreateCommandHandler> _logger;

        public UserCreateCommandHandler(IFrameRepository frameRepository, IMapper mapper, ILogger<UserCreateCommandHandler> logger)
        {
            _frameRepository = frameRepository ?? throw new ArgumentNullException(nameof(frameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<UserProfile> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var value = _mapper.Map<UserProfile>(request);
            value.CreatedDate = DateTime.Now;
            var response = await _frameRepository.CreateUser(value);
            return response;
        }
    }
}
