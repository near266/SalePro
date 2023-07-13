using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using InteractiveSvc.Domain.Entities;
using InteractiveSvc.Application.Contract.Contracts;
using InteractiveSvc.Application.Persistences;
using InteractiveSvc.Application.DTO;

namespace InteractiveSvc.Application.Commands.UserProfiles
{
    public class UserUpdateCommand : IRequest<UserProfile>
    {
        public string Id { get; set; }
        public string? Fullname { get; set; }
        public DateTime? DOB { get; set; }
        public string? Avatar { get; set; }
        public string? CoverImage { get; set; }
        public string? Position { get; set; }
        public string? Company { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public int? Role { get; set; }
    }
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, UserProfile>
    {
        private readonly IFrameRepository _frameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserUpdateCommandHandler> _logger;

        public UserUpdateCommandHandler(IFrameRepository frameRepository, IMapper mapper, ILogger<UserUpdateCommandHandler> logger)
        {
            _frameRepository = frameRepository ?? throw new ArgumentNullException(nameof(frameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<UserProfile> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var value = _mapper.Map<UserUpdateDTO>(request);
            var response = await _frameRepository.UpdateUser(request.Id, value, cancellationToken);
            return response;
        }
    }
}
