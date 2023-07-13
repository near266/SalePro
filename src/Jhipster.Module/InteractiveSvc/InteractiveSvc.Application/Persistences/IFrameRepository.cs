using InteractiveSvc.Application.DTO;
using InteractiveSvc.Domain.Entities;
using InteractiveSvc.Domain.Entities.Events;
using InteractiveSvc.Domain.Entities.Meetings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveSvc.Application.Persistences
{
    public interface IFrameRepository
    {
        Task<UserProfile> CreateUser(UserProfile user, CancellationToken cancellationToken = default);
        Task<UserProfile> DetailUser(string? Id);
        Task<UserProfile> UpdateUser(string Id, UserUpdateDTO update, CancellationToken cancellationToken);

        Task<Meeting> CreateMeeting(Meeting meeting, CancellationToken cancellationToken);
        Task<Meeting> UpdateMeeting(Guid Id, MeetingUpdateDTO update, CancellationToken cancellationToken);
        Task<Meeting> DetailMeeting(Guid? Id);

        Task<Event> CreateEvent(Event create, CancellationToken cancellationToken);
        Task<Event> UpdateEvent(Guid Id, EventUpdateDTO update, CancellationToken cancellationToken);
        Task<List<Event>> ListEvent(string? Title, string? Description, bool? IsOpening, DateTime? SearchDate, DateTime? StartDate, DateTime? EndDate);
        Task<Event> DetailEvent(Guid Id);

        Task<EventJoin> CreateEventJoin(EventJoin request, CancellationToken cancellationToken);
        Task<List<EventJoin>> EventJoinList(Guid? Id);
        Task<List<EventJoin>> EventJoinListById(string? Id);
        Task<List<EventJoin>> EventListJoined(string Id);

    }
}
