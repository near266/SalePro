using InteractiveSvc.Domain.Entities;
using InteractiveSvc.Domain.Entities.Events;
using InteractiveSvc.Domain.Entities.Meetings;
using Microsoft.EntityFrameworkCore;

namespace InteractiveSvc.Domain.Abstractions
{
    public interface IInteractiveSvcDbContext
    {
    

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
