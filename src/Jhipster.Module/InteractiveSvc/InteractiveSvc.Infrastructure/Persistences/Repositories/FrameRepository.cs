using AutoMapper;
using InteractiveSvc.Domain.Entities;
using InteractiveSvc.Domain.Entities.Meetings;
using Microsoft.EntityFrameworkCore;
using InteractiveSvc.Application.Persistences;
using InteractiveSvc.Domain.Abstractions;
using InteractiveSvc.Shared.Constants;
using InteractiveSvc.Application.DTO;
using Microsoft.Extensions.Logging;
using InteractiveSvc.Domain.Entities.Events;

namespace InteractiveSvc.Infrastructure.Persistences.Repositories
{
    public class FrameRepository : IFrameRepository
    {
        private readonly IInteractiveSvcDbContext _interactiveDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<FrameRepository> _logger;

        public FrameRepository(IInteractiveSvcDbContext interactiveDbContext, IMapper mapper, ILogger<FrameRepository> logger)
        {
            _interactiveDbContext = interactiveDbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserProfile> CreateUser(UserProfile user, CancellationToken cancellationToken = default)
        {
            var check = await _interactiveDbContext.UserProfiles.Where(m => m.Username.ToUpper() == user.Username.ToUpper()).FirstOrDefaultAsync();
            if (check != null) _logger.LogError("Already Existed");
            user.Fullname = user.Username;
            user.Role = 0;
            user.Avatar = "https://cdn.eztek.net/TrueConnect/Images/AvatarDefault_638191530952395772_ORIGIN.png";
            user.CoverImage = "https://cdn.eztek.net/TrueConnect/Images/default-cover_638241704781692527_ORIGIN.png";
            user.CreatedDate = DateTime.Now;
            user.LastModifiedDate = DateTime.Now;
            await _interactiveDbContext.UserProfiles.AddAsync(user);
            await _interactiveDbContext.SaveChangesAsync(cancellationToken);
            return user;
        }
        public async Task<UserProfile> DetailUser(string? Id)
        {
            return await _interactiveDbContext.UserProfiles.Where(m => m.Id == Id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<UserProfile> UpdateUser(string Id, UserUpdateDTO update, CancellationToken cancellationToken)
        {
            var check = await _interactiveDbContext.UserProfiles.Where(m => m.Id == Id).FirstOrDefaultAsync();
            if (check == null) return null;
            _mapper.Map(update, check);
            check.LastModifiedDate = DateTime.Now;
            await _interactiveDbContext.SaveChangesAsync(cancellationToken);

            if(!string.IsNullOrEmpty(update.Avatar) || !string.IsNullOrEmpty(update.Fullname))
            {
                var checkEventJoin = await _interactiveDbContext.EventJoins.Where(m => m.UserId == Id).ToListAsync();
                foreach (var item in checkEventJoin)
                {
                    item.Avatar = string.IsNullOrEmpty(update.Avatar) ? check.Avatar : update.Avatar;
                    item.Avatar = string.IsNullOrEmpty(update.Fullname) ? check.Fullname : update.Fullname;
                }
                _interactiveDbContext.EventJoins.UpdateRange(checkEventJoin);
                await _interactiveDbContext.SaveChangesAsync(cancellationToken);
            }
            return check;
        }

        public async Task<Meeting> CreateMeeting(Meeting meeting, CancellationToken cancellationToken)
        {
            meeting.CreatedDate = DateTime.Now;
            meeting.LastModifiedDate = DateTime.Now;
            await _interactiveDbContext.Meetings.AddAsync(meeting);
            await _interactiveDbContext.SaveChangesAsync(cancellationToken);
            return meeting;
        }

        public async Task<Meeting> UpdateMeeting(Guid Id, MeetingUpdateDTO update, CancellationToken cancellationToken)
        {
            var check = await _interactiveDbContext.Meetings.Where(m => m.Id == Id).FirstOrDefaultAsync();
            if (check == null) _logger.LogError("Could not found");
            _mapper.Map(update, check);
            check.LastModifiedDate = DateTime.Now;
            await _interactiveDbContext.SaveChangesAsync(cancellationToken);
            return check;
        }
        public async Task<Meeting> DetailMeeting(Guid? Id)
        {
            return await _interactiveDbContext.Meetings.Where(m => m.Id == Id).AsNoTracking().FirstOrDefaultAsync();
        }



        public async Task<Event> CreateEvent(Event create, CancellationToken cancellationToken)
        {
            create.CreatedDate = DateTime.Now;
            create.LastModifiedDate = DateTime.Now;
            create.Status = 0;
            create.Quantity = 0;
            await _interactiveDbContext.Events.AddAsync(create);
            await _interactiveDbContext.SaveChangesAsync(cancellationToken);
            return create;
        }
        public async Task<Event> UpdateEvent(Guid Id, EventUpdateDTO update, CancellationToken cancellationToken)
        {
            var check = await _interactiveDbContext.Events.Where(m => m.Id == Id).FirstOrDefaultAsync();
            if (check == null) _logger.LogError("Could not found");
            _mapper.Map(update, check);
            check.LastModifiedDate = DateTime.Now;
            await _interactiveDbContext.SaveChangesAsync(cancellationToken);
            return check;
        }

        public async Task<List<Event>> ListEvent(string? Title, string? Description, bool? IsOpening, DateTime? SearchDate, DateTime? StartDate, DateTime? EndDate)
        {   
            var list = await _interactiveDbContext.Events.AsNoTracking().ToListAsync();
            if (!string.IsNullOrEmpty(IsOpening.ToString()) && IsOpening == true)
            {  
                list = list.Where(m => m.Status == 1).ToList();
                list = list.OrderByDescending(m => m.StartDate).ToList();
                return list;

            }
            if ((!string.IsNullOrEmpty(StartDate.ToString()) && StartDate != DateTime.MinValue) && (!string.IsNullOrEmpty(EndDate.ToString()) && StartDate != DateTime.MinValue))
            {
                list = list.Where(m => m.StartDate >= StartDate && m.StartDate <= EndDate).ToList();
                /*
                list = list.Where(m => m.StartDate.Value.Year >= StartDate.Value.Year && m.StartDate.Value.Month >= StartDate.Value.Month && m.StartDate.Value.Day >= StartDate.Value.Day &&
                                        m.EndDate.Value.Year <= EndDate.Value.Year && m.EndDate.Value.Month <= EndDate.Value.Month && m.EndDate.Value.Day <= EndDate.Value.Day).ToList();
                */list = list.OrderByDescending(m => m.StartDate).ToList(); 
                return list;

            }
            if (!string.IsNullOrEmpty(SearchDate.ToString()) && SearchDate != DateTime.MinValue)
            {
                list = list.Where(m => m.StartDate.Value.Year == SearchDate.Value.Year && m.StartDate.Value.Month == SearchDate.Value.Month && m.StartDate.Value.Day == SearchDate.Value.Day).ToList();
                list = list.OrderByDescending(m => m.StartDate).ToList();
                return list;
            }
            if (!string.IsNullOrEmpty(Title)) list = list.Where(m => m.Title.ToUpper().Contains(Title.ToUpper())).ToList();
            if (!string.IsNullOrEmpty(Description) && Description.Split(' ').Length < 30) list = list.Where(m => m.Description.ToUpper().Contains(Description.ToUpper())).ToList();
            list = list.OrderByDescending(m => m.StartDate).ToList();
            return list;
        }

        public async Task<Event> DetailEvent(Guid Id)
        {
            return await _interactiveDbContext.Events.Where(m => m.Id == Id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<EventJoin> CreateEventJoin(EventJoin request, CancellationToken cancellationToken)
        {
            request.CreatedDate = DateTime.Now;
            request.CheckedInDate = DateTime.Now;
            var user = await _interactiveDbContext.UserProfiles.Where(m => m.Id == request.UserId).AsNoTracking().FirstOrDefaultAsync();
            var checkEvent = await _interactiveDbContext.Events.Where(m => m.Id == request.EventId).AsNoTracking().FirstOrDefaultAsync();
            var check = await _interactiveDbContext.EventJoins.Where(m => m.UserId == request.UserId && m.EventId == request.EventId).Include(m => m.Events).AsNoTracking().FirstOrDefaultAsync();
            if (check != null) return check;
            request.Fullname = string.IsNullOrEmpty(user.Fullname) ? user.Username : user.Fullname;
            request.EventTitle = checkEvent.Title;
            request.Avatar = string.IsNullOrEmpty(user.Avatar) ? "https://cdn.eztek.net/TrueConnect/Images/AvatarDefault_638191530952395772_ORIGIN.png" : user.Avatar;
            checkEvent.Quantity++;
            _interactiveDbContext.EventJoins.Add(request);
            _interactiveDbContext.Events.Update(checkEvent);
            await _interactiveDbContext.SaveChangesAsync(cancellationToken);
            return check;
        }

        public async Task<List<EventJoin>> EventJoinList(Guid? Id)
        {
            var list = await _interactiveDbContext.EventJoins.Where(m => m.EventId == Id).AsNoTracking().ToListAsync();
            list.OrderByDescending(m => m.CheckedInDate);
            return list;
        }
        public async Task<List<EventJoin>> EventJoinListById(string? Id)
        {
            var list = await _interactiveDbContext.EventJoins.Where(m => m.UserId == Id).AsNoTracking().ToListAsync();
            list.OrderByDescending(m => m.CheckedInDate);
            return list;
        }

        public async Task<List<EventJoin>> EventListJoined(string? Id)
        {
            var list = await _interactiveDbContext.EventJoins.Where(m => m.UserId == Id).AsNoTracking().ToListAsync();
            list.OrderByDescending(m => m.CheckedInDate);
            return list;
        }


    }
}
