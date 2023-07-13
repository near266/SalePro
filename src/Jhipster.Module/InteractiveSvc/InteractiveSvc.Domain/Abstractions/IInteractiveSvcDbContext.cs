using InteractiveSvc.Domain.Entities;
using InteractiveSvc.Domain.Entities.Events;
using InteractiveSvc.Domain.Entities.Meetings;
using Microsoft.EntityFrameworkCore;

namespace InteractiveSvc.Domain.Abstractions
{
    public interface IInteractiveSvcDbContext
    {
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingJoin> MeetingJoins { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventJoin> EventJoins { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
