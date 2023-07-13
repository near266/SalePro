using InteractiveSvc.Domain.Abstractions;
using InteractiveSvc.Domain.Entities;
using InteractiveSvc.Domain.Entities.Events;
using InteractiveSvc.Domain.Entities.Meetings;
using Jhipster.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace InteractiveSvc.Infrastructure.Persistences
{
    public class InteractiveSvcDbContext : ModuleDbContext, IInteractiveSvcDbContext
    {
        public InteractiveSvcDbContext(DbContextOptions<InteractiveSvcDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(true, cancellationToken);
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingJoin> MeetingJoins { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventJoin> EventJoins { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }


    }
}
